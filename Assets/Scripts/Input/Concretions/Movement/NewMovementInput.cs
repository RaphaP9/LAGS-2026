using System;
using UnityEngine;

public class NewMovementInput : MovementInput
{
    private PlayerInputActions playerInputActions;

    protected override void Awake()
    {
        base.Awake();
        InitializePlayerInputActions();
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.Enable();
    }

    public override bool CanProcessMovementInput()
    {
        if (ScenesManager.Instance.SceneState != ScenesManager.State.Idle) return false;

        return true;
    }

    public override Vector2 GetDirectionVectorNormalized()
    {
        if (!CanProcessMovementInput()) return Vector2.zero;

        Vector2 inputVector = playerInputActions.Movement.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }
}

