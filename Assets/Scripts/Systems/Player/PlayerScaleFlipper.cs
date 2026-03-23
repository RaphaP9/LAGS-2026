using UnityEngine;

public class PlayerScaleFlipper : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform transformToFlip;
    [SerializeField] private PlayerFacingDirectionHandler playerFacingDirectionHandler;

    [Header("Settings")]
    [SerializeField] private bool enableFlip;

    private bool facingRight = true;

    private void Update()
    {
        HandleFlipDueToFacing();
    }

    private void HandleFlipDueToFacing()
    {
        if (!enableFlip) return;

        if (playerFacingDirectionHandler.IsFacingRight)
        {
            CheckFlipRight();
        }

        if (!playerFacingDirectionHandler.IsFacingRight)
        {
            CheckFlipLeft();
        }
    }

    private void CheckFlipRight()
    {
        if (facingRight) return;

        FlipRight();

        facingRight = true;
    }

    private void CheckFlipLeft()
    {
        if (!facingRight) return;

        FlipLeft();

        facingRight = false;
    }

    private void FlipRight()
    {
        transformToFlip.localScale = new Vector3(1f, 1f, 1f);
    }

    private void FlipLeft()
    {
        transformToFlip.localScale = new Vector3(-1f, 1f, 1f);
    }

}
