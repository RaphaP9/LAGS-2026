using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueInput : DialogueInput
{
    private PlayerInputActions playerInputActions;

    private void OnDisable()
    {
        playerInputActions.Dialogue.Disable();
    }

    protected override void Awake()
    {
        base.Awake();
        InitializePlayerInputActions();
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Dialogue.Enable();
    }

    public override bool CanProcessInput()
    {
        if(PauseManager.Instance != null)
        {
            if (PauseManager.Instance.GamePaused) return false;
        }

        if (GameManager.Instance != null)
        {
            if(GameManager.Instance.GameState == GameManager.State.Exploring) return false;
            if(GameManager.Instance.GameState == GameManager.State.DayEnd) return false;
        }
        
        return true;
    }

    public override bool GetSkipDown()
    {
        if (!CanProcessInput()) return false;

        bool input = playerInputActions.Dialogue.Skip.WasPerformedThisFrame();

        return input;

    }

}
