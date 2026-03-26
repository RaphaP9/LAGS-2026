using UnityEngine;
using System;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MovementInput movementInput;
    [Space]
    [SerializeField] private CheckGround checkGround;
    [SerializeField] private CheckWall checkWall;

    [Header("Speed Settings")]
    [SerializeField, Range(1.5f, 10f)] private float moveSpeed = 2f;
    [Space]
    [SerializeField] private bool flattenSpeedOnSlopes;
    [SerializeField, Range(0f, 10f)] private float flattenSpeedThreshold;

    [Header("Smooth Settings")]
    [SerializeField, Range(1f, 100f)] private float smoothInputFactor = 5f;
    [SerializeField, Range(1f, 100f)] private float smoothVelocityFactor = 5f;
    [SerializeField, Range(1f, 100f)] private float smoothDirectionFactor = 5f;

    [Header("State")]
    [SerializeField] private State state;
    private enum State { NotMoving, Moving }

    private Rigidbody _rigidbody;

    public Vector2 DirectionInputVector => movementInput.GetDirectionVectorNormalized();

    private float desiredSpeed;
    private float smoothCurrentSpeed;

    public Vector3 LastNonZeroInput { get; private set; }
    public Vector3 FixedLastNonZeroInput { get; private set; }

    private Vector3 smoothDirectionInputVector;

    public Vector3 FinalMoveDir { get; private set; }
    public Vector3 SmoothFinalMoveDir { get; private set; }
    public Vector3 FinalMoveVector { get; private set; }
    public bool MovementEnabled => movementEnabled;

    public static event EventHandler OnPlayerStopMoving;
    public static event EventHandler OnPlayerStartMoving;

    private const float PROYECTION_MAGNITUDE_THRESHOLD = 0.1f;

    private bool movementEnabled = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SetSingleton();
    }

    private void Start()
    {
        SetMovementState(State.NotMoving);
    }

    private void Update()
    {
        HandleMovement();
        HandleMovementState();
    }

    private void FixedUpdate()
    {
        ApplyHorizontalMovement();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one PlayerHorizontalMovement instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        if (!movementEnabled) return;

        CalculateDesiredSpeed();
        SmoothSpeed();

        CalculateLastNonZeroDirectionInput();
        FixDirectionVectorDueToWalls();

        SmoothDirectionInputVector();

        
        CalculateDesiredMovementDirection();
        SmoothDirectionVector();

        CalculateFinalMovement();
        
    }

    private void CalculateDesiredSpeed()
    {
        desiredSpeed = CanMove() ? moveSpeed : 0f;
    }

    private bool CanMove()
    {
        if (DirectionInputVector == Vector2.zero) return false;
        if (!FixDirectionVectorDueToWalls()) return false;
        if (checkWall.HitCorner && checkWall.HitWall && !checkGround.OnSlope) return false;

        return true;
    }

    private void SmoothSpeed()
    {
        smoothCurrentSpeed = Mathf.Lerp(smoothCurrentSpeed, desiredSpeed, Time.deltaTime * smoothVelocityFactor);
    }

    private void CalculateLastNonZeroDirectionInput() => LastNonZeroInput = GeneralUtilities.Vector2ToVector3InZ(DirectionInputVector != Vector2.zero ? DirectionInputVector : LastNonZeroInput);

    private bool FixDirectionVectorDueToWalls()
    {
        if (!checkWall.HitWall)
        {
            FixedLastNonZeroInput = LastNonZeroInput;
            return true;
        }
      
        Vector3 wallNormal = checkWall.GetDiagonalWallInfo().normal;
        Vector3 proyectionOnNormal = Vector3.Project(LastNonZeroInput, wallNormal);

        Vector3 proyectionOnWall = LastNonZeroInput - proyectionOnNormal;

        if (proyectionOnWall.magnitude < PROYECTION_MAGNITUDE_THRESHOLD) return false;

        FixedLastNonZeroInput = proyectionOnWall.normalized;
        return true;      
    }

    private void SmoothDirectionInputVector() => smoothDirectionInputVector = Vector3.Lerp(smoothDirectionInputVector, FixedLastNonZeroInput, Time.deltaTime * smoothInputFactor);

    private void CalculateDesiredMovementDirection()
    {
        Vector3 flattenDir = FlattenVectorOnSlopes(smoothDirectionInputVector);

        FinalMoveDir = flattenDir;
    }

    private void SmoothDirectionVector() => SmoothFinalMoveDir = Vector3.Slerp(SmoothFinalMoveDir, FinalMoveDir, Time.deltaTime * smoothDirectionFactor);

    private Vector3 FlattenVectorOnSlopes(Vector3 vectorToFlat)
    {
        if (!checkGround.OnSlope) return vectorToFlat;
        if (!flattenSpeedOnSlopes) return vectorToFlat;
        if (FinalMoveVector.magnitude < flattenSpeedThreshold) return vectorToFlat;

        vectorToFlat = Vector3.ProjectOnPlane(vectorToFlat, checkGround.SlopeNormal);

        return vectorToFlat;
    }

    private void CalculateFinalMovement()
    {
        Vector3 finalVector = SmoothFinalMoveDir * smoothCurrentSpeed;

        Vector3 roundedFinalVector = Vector3.zero;

        roundedFinalVector.x = Math.Abs(finalVector.x) < 0.01f ? 0f : finalVector.x;
        roundedFinalVector.z = Math.Abs(finalVector.z) < 0.01f ? 0f : finalVector.z;


        FinalMoveVector = new Vector3(roundedFinalVector.x, 0f, roundedFinalVector.z);
    }

    private void ApplyHorizontalMovement()
    {
        if (!movementEnabled) return;
        _rigidbody.linearVelocity = new Vector3(FinalMoveVector.x, _rigidbody.linearVelocity.y, FinalMoveVector.z);
    }

    public bool HasMovementInput() => DirectionInputVector != Vector2.zero;

    #region MovementStates
    private void HandleMovementState()
    {
        switch (state)
        {
            case State.NotMoving:
                NotMovingLogic();
                break;
            case State.Moving:
                WalkingLogic();
                break;
        }
    }

    private void NotMovingLogic()
    {
        if (!checkGround.IsGrounded) return;

        if (desiredSpeed == moveSpeed)
        {
            SetMovementState(State.Moving);
            OnPlayerStartMoving?.Invoke(this, EventArgs.Empty);
            return;
        }
    }

    private void WalkingLogic()
    {
        if (!checkGround.IsGrounded || desiredSpeed == 0f)
        {
            SetMovementState(State.NotMoving);
            OnPlayerStopMoving?.Invoke(this, EventArgs.Empty);
            return;
        }
    }

    private void StopMovement()
    {
        FinalMoveVector = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void SetMovementState(State state) => this.state = state;
    #endregion

    public void StopPlayerForSeconds(float seconds)
    {
        StopAllCoroutines();
        StartCoroutine(StopPlayerForSecondsCoroutine(seconds));
    }

    private IEnumerator StopPlayerForSecondsCoroutine(float seconds)
    {
        movementEnabled = false;

        StopMovement();

        yield return new WaitForSeconds(seconds);
        movementEnabled = true;
    }
}
