using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInteractionInput : InteractionInput
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
        playerInputActions.Interaction.Enable();
    }

    public override bool CanProcessInteractionInput()
    {
        if (playerInputActions == null) return false;

        if (ScenesManager.Instance.SceneState != ScenesManager.State.Idle) return false;
        return true;
    }

    public override bool GetInteractionDown()
    {
        if (!CanProcessInteractionInput()) return false;

        bool interactionInput = playerInputActions.Interaction.Interact.WasPerformedThisFrame();
        return interactionInput;
    }

    public override bool GetInteractionUp()
    {
        if (!CanProcessInteractionInput()) return false;

        bool interactionInput = playerInputActions.Interaction.Interact.WasReleasedThisFrame();
        return interactionInput;
    }

    public override bool GetInteractionHold()
    {
        if (!CanProcessInteractionInput()) return false;

        bool interactionInput = playerInputActions.Interaction.Interact.IsPressed();
        return interactionInput;
    }
}
