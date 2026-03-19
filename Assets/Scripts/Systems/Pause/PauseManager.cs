using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [Header("Runtime Filled")]
    [SerializeField] private List<PauseUIBase> pauseUIBaseList;
    [SerializeField] private bool gamePaused;

    public static event EventHandler OnGamePaused;
    public static event EventHandler OnGameResumed;

    public bool GamePaused => gamePaused;

    private void OnEnable()
    {
        PauseUIBase.OnPauseUIBaseOpen += PauseUIBase_OnPauseUIBaseOpen;
        PauseUIBase.OnPauseUIBaseClose += PauseUIBase_OnPauseUIBaseClose;
    }

    private void OnDisable()
    {
        PauseUIBase.OnPauseUIBaseOpen -= PauseUIBase_OnPauseUIBaseOpen;
        PauseUIBase.OnPauseUIBaseClose -= PauseUIBase_OnPauseUIBaseClose;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one PauseManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }
    private void InitializeVariables()
    {
        SetGamePaused(false);
        AudioListener.pause = false;
    }

    #region PauseUIBase List Logic
    private void AddPauseUIToListLogic(PauseUIBase pauseUIBase)
    {
        pauseUIBaseList.Add(pauseUIBase);

        if (!gamePaused) PauseGame();
    }

    private void RemovePauseUIToListLogic(PauseUIBase pauseUIBase)
    {
        pauseUIBaseList.Remove(pauseUIBase);

        if (pauseUIBaseList.Count <= 0 && gamePaused) ResumeGame();
    }
    #endregion

    #region Pause & Resume

    public void PauseGame()
    {
        if (gamePaused) return;

        OnGamePaused?.Invoke(this, EventArgs.Empty);
        SetPauseTimeScale();
        SetGamePaused(true);
        AudioListener.pause = false;
    }

    public void ResumeGame()
    {
        if (!gamePaused) return;

        OnGameResumed?.Invoke(this, EventArgs.Empty);
        SetResumeTimeScale();
        SetGamePaused(false);
        AudioListener.pause = false;
    }
    #endregion

    #region Setters
    private void SetGamePaused(bool gamePaused) => this.gamePaused = gamePaused;
    private void SetPauseTimeScale() => Time.timeScale = 0f;
    private void SetResumeTimeScale() => Time.timeScale = 1f;
    #endregion

    #region Subscriptions
    private void PauseUIBase_OnPauseUIBaseOpen(object sender, PauseUIBase.OnPauseUIEventArgs e)
    {
        AddPauseUIToListLogic(e.pauseUIBase);
    }
    private void PauseUIBase_OnPauseUIBaseClose(object sender, PauseUIBase.OnPauseUIEventArgs e)
    {
        RemovePauseUIToListLogic(e.pauseUIBase);
    }
    #endregion
}
