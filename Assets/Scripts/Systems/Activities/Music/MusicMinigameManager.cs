using System;
using System.Collections;
using UnityEngine;

public class MusicMinigameManager : MonoBehaviour
{
    public static MusicMinigameManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private ActivitiesManager activitiesManager;
    [SerializeField] private MinigameAskPlayUI minigameAskPlayUI;
    [SerializeField] private MusicMinigameUI musicMinigameUI;

    [Header("Settings")]
    [SerializeField, Range(0f, 5f)] private float startingMinigameTime;
    [SerializeField, Range(0f, 5f)] private float waitForPartitionTime;
    [SerializeField, Range(0f, 5f)] private float minigameIntervalTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public static event EventHandler OnWaitForPartition;
    public static event EventHandler OnPlayingMusic;
    public static event EventHandler OnMusicSuccess;
    public static event EventHandler OnMusicFail;
    public static event EventHandler OnMusicInterval;
    public static event EventHandler OnMusicIntervalSuccess;
    public static event EventHandler OnMusicIntervalFail;

    public enum State { StartingMinigame, AskForPlay, WaitForPartition, PlayingMusic, MinigameInterval }

    private bool minigamePlayTrigger = false;
    private bool musicSuccess = false;
    private bool musicFail = false;

    private void OnEnable()
    {
        minigameAskPlayUI.OnMinigamePlay += MinigameAskPlayUI_OnMinigamePlay;
        musicMinigameUI.OnMusicSuccess += MusicMinigameUI_OnMusicSuccess;
        musicMinigameUI.OnMusicFail += MusicMinigameUI_OnMusicFail;
    }

    private void OnDisable()
    {
        minigameAskPlayUI.OnMinigamePlay -= MinigameAskPlayUI_OnMinigamePlay;
        musicMinigameUI.OnMusicSuccess -= MusicMinigameUI_OnMusicSuccess;
        musicMinigameUI.OnMusicFail -= MusicMinigameUI_OnMusicFail;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        StartCoroutine(MusicMinigameCoroutine());
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one MusicMinigameManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private IEnumerator MusicMinigameCoroutine()
    {
        SetState(State.StartingMinigame);

        yield return new WaitForSeconds(startingMinigameTime);

        while (true)
        {
            SetState(State.AskForPlay);

            minigameAskPlayUI.ShowUI();

            yield return new WaitUntil(() => minigamePlayTrigger);
            minigamePlayTrigger = false;

            minigameAskPlayUI.HideUI();

            SetState(State.WaitForPartition);

            OnWaitForPartition?.Invoke(this, EventArgs.Empty);

            yield return new WaitForSeconds(waitForPartitionTime);

            SetState(State.PlayingMusic);

            OnPlayingMusic?.Invoke(this, EventArgs.Empty);

            musicMinigameUI.StartMusicGame();

            yield return new WaitUntil(() => musicSuccess || musicFail);

            if (musicSuccess) OnMusicSuccess?.Invoke(this, EventArgs.Empty);

            if (musicFail) OnMusicFail?.Invoke(this, EventArgs.Empty);

            SetState(State.MinigameInterval);

            yield return new WaitForSeconds(minigameIntervalTime);

            OnMusicInterval?.Invoke(this, EventArgs.Empty);

            if (musicSuccess) OnMusicIntervalSuccess?.Invoke(this, EventArgs.Empty);
            if (musicFail) OnMusicIntervalFail?.Invoke(this, EventArgs.Empty);

            musicSuccess = false;
            musicFail = false;
        }
    }

    private void SetState(State state) => this.state = state;

    #region Subscriptions
    private void MinigameAskPlayUI_OnMinigamePlay(object sender, System.EventArgs e)
    {
        minigamePlayTrigger = true;
    }

    private void MusicMinigameUI_OnMusicSuccess(object sender, System.EventArgs e)
    {
        musicSuccess = true;
    }

    private void MusicMinigameUI_OnMusicFail(object sender, System.EventArgs e)
    {
        musicFail = true;
    }

    #endregion
}
