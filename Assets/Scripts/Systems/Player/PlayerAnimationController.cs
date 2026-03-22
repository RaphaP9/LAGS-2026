using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<Animator> animatorList;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerFacingDirectionHandler playerFacingDirectionHandler;

    protected const string SPEED_FLOAT = "Speed";
    protected const string FACE_X_FLOAT = "FaceX";
    protected const string FACE_Y_FLOAT = "FaceY";

    protected virtual void Update()
    {
        HandleSpeedBlend();
        HandleFacingBlend();
    }

    private void HandleSpeedBlend()
    {
        foreach(Animator animator in animatorList)
        {
           animator.SetFloat(SPEED_FLOAT, playerMovement.FinalMoveVector.magnitude);
        }
    }

    private void HandleFacingBlend()
    {
        foreach (Animator animator in animatorList)
        {
            animator.SetFloat(FACE_X_FLOAT, playerFacingDirectionHandler.CurrentFacingDirection.x);
            animator.SetFloat(FACE_Y_FLOAT, playerFacingDirectionHandler.CurrentFacingDirection.y);
        }
    }
}
