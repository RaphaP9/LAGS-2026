using UnityEngine;

public class NewMinigamesInput : MinigamesInput
{
    private PlayerInputActions playerInputActions;

    private void OnDisable()
    {
        playerInputActions.Minigames.Disable();
    }

    protected override void Awake()
    {
        base.Awake();
        InitializePlayerInputActions();
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Minigames.Enable();
    }

    public override bool CanProcessInput()
    {
        return true;
    }

    public override bool GetFishDown()
    {
        if (!CanProcessInput()) return false;

        bool input = playerInputActions.Minigames.Fish.WasPerformedThisFrame();

        return input;
    }

    public override bool GetWeaveDown()
    {
        if (!CanProcessInput()) return false;

        bool input = playerInputActions.Minigames.Weave.WasPerformedThisFrame();

        return input;
    }


}
