using System;
using UnityEngine;

public class PlayerFacingDirectionHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    public bool IsFacingRight { get; private set; } = true;

    private void Update()
    {
        HandleFacing();
    }

    private void HandleFacing()
    {
        if (playerMovement.FixedLastNonZeroInput.x > 0)
        {
            IsFacingRight = true;
        }

        if (playerMovement.FixedLastNonZeroInput.x < 0)
        {
            IsFacingRight = false;
        }
    }
}
