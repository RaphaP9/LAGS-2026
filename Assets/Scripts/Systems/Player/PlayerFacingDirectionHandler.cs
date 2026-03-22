using System;
using UnityEngine;

public class PlayerFacingDirectionHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Settings")]
    [SerializeField] private FacingType facingType;
    [SerializeField, Range(0.5f, 10f)] private float minimumRigidbodyVelocity;

    [Header("RuntimeFilled")]
    [SerializeField] private Vector2Int currentFacingDirection;

    private enum FacingType { Rigidbody, LastNonZeroInput }


    public bool IsFacingRight { get; private set; } = true;

    public Vector2Int CurrentFacingDirection => currentFacingDirection;

    private void Update()
    {
        HandleFacing();
        HandleFacingRight();
    }

    private void HandleFacing()
    {
        switch (facingType)
        {
            case FacingType.Rigidbody:
            default:
                HandleFacingDirectionByRigidbody();
                break;
            case FacingType.LastNonZeroInput:
                HandleFacingDirectionByLastNonZeroInput();
                break;

        }
    }

    private void HandleFacingDirectionByRigidbody()
    {
        if (_rigidbody.linearVelocity.magnitude < minimumRigidbodyVelocity) return;

        Vector3 rawFacingDirection = _rigidbody.linearVelocity.normalized;
        rawFacingDirection = GeneralUtilities.Vector3ToVector2InZ(rawFacingDirection);

        currentFacingDirection = GeneralUtilities.ClampVector2To8Direction(rawFacingDirection);
    }

    private void HandleFacingDirectionByLastNonZeroInput()
    {
        Vector3 rawFacingDirection = playerMovement.FixedLastNonZeroInput;
        rawFacingDirection = GeneralUtilities.Vector3ToVector2InZ(rawFacingDirection);

        currentFacingDirection = GeneralUtilities.ClampVector2To8Direction(rawFacingDirection);
    }

    private void HandleFacingRight()
    {
        if (currentFacingDirection.x > 0)
        {
            IsFacingRight = true;
        }

        if (currentFacingDirection.x < 0)
        {
            IsFacingRight = false;
        }
    }
}
