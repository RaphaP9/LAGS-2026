using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [Header("Settings")]
    [SerializeField] private State startingState;

    [Header("States")]
    [SerializeField] private State state;
    [SerializeField] private State previousState;

    public enum State {Exploring, Dialogue, DayEnd}

    public State GameState => state;

    private void OnEnable()
    {
        DialogueManager.OnDialogueBegin += DialogueManager_OnDialogueBegin;
        DialogueManager.OnDialogueEnd += DialogueManager_OnDialogueEnd;

        DayTimeManager.OnDayEnd += DayTimeManager_OnDayEnd;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueBegin -= DialogueManager_OnDialogueBegin;
        DialogueManager.OnDialogueEnd -= DialogueManager_OnDialogueEnd;

        DayTimeManager.OnDayEnd -= DayTimeManager_OnDayEnd;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        SetGameState(startingState);
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one GameManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }


    #region States
    private void SetGameState(State state)
    {
        SetPreviousState(this.state);
        this.state = state;
    }

    private void SetPreviousState(State state)
    {
        previousState = state;
    }
    #endregion

    #region Subscriptions
    private void DialogueManager_OnDialogueBegin(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        SetGameState(State.Dialogue);
    }

    private void DialogueManager_OnDialogueEnd(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        SetGameState(State.Exploring);
    }

    private void DayTimeManager_OnDayEnd(object sender, DayTimeManager.OnDayEventArgs e)
    {
        SetGameState(State.DayEnd);
    }
    #endregion
}
