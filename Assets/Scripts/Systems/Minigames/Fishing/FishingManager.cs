using System;
using System.Collections;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MinigameAskPlayUI minigameAskPlayUI;
    [SerializeField] private FishingUI fishingUI;
    [SerializeField] private ActivitiesManager activitiesManager;

    [Header("Settings")]
    [SerializeField, Range(0f,5f)] private float startingMinigameTime;
    [SerializeField, Range(0f, 5f)] private float minWaitForFishTime;
    [SerializeField, Range(0f, 5f)] private float maxWaitForFishTime;
    [SerializeField, Range(0f, 5f)] private float minigameIntervalTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public static event EventHandler OnWaitForFish;
    public static event EventHandler OnPullingRod;
    public static event EventHandler OnFishingSuccess;
    public static event EventHandler OnFishingFail;
    public static event EventHandler OnFishingInterval;

    public enum State { StartingMinigame, AskForPlay, WaitingForFish, PullingFishingRod, MinigameInterval}

    private bool mingamePlayTrigger = false;
    private bool fishingSuccess = false;
    private bool fishingFail = false;

    private void OnEnable()
    {
        minigameAskPlayUI.OnMinigamePlay += MinigameAskPlayUI_OnMinigamePlay;
        fishingUI.OnFishingSuccess += FishingUI_OnFishingSuccess;
        fishingUI.OnFishingFail += FishingUI_OnFishingFail;
    }

    private void OnDisable()
    {
        minigameAskPlayUI.OnMinigamePlay -= MinigameAskPlayUI_OnMinigamePlay;
        fishingUI.OnFishingSuccess -= FishingUI_OnFishingSuccess;
        fishingUI.OnFishingFail -= FishingUI_OnFishingFail;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        StartCoroutine(FishingMinigameCoroutine());
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one FishingManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private IEnumerator FishingMinigameCoroutine()
    {
        SetState(State.StartingMinigame);

        yield return new WaitForSeconds(startingMinigameTime);

        while (true)
        {
            SetState(State.AskForPlay);

            minigameAskPlayUI.ShowUI();

            yield return new WaitUntil(() => mingamePlayTrigger);
            mingamePlayTrigger = false;

            minigameAskPlayUI.HideUI();

            SetState(State.WaitingForFish);

            OnWaitForFish?.Invoke(this, EventArgs.Empty);

            float timeToWait = GeneralUtilities.GetRandomBetweenTwoFloats(minWaitForFishTime, maxWaitForFishTime);

            yield return new WaitForSeconds(timeToWait);

            SetState(State.PullingFishingRod);

            OnPullingRod?.Invoke(this, EventArgs.Empty);

            fishingUI.StartFishingUIGame();

            yield return new WaitUntil(() => fishingSuccess || fishingFail);

            if (fishingSuccess) OnFishingSuccess?.Invoke(this, EventArgs.Empty);

            if (fishingFail) OnFishingFail?.Invoke(this,EventArgs.Empty);

            fishingSuccess = false;
            fishingFail = false;

            SetState(State.MinigameInterval);

            yield return new WaitForSeconds(minigameIntervalTime);

            OnFishingInterval?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetState(State state) => this.state = state;

    #region Subscriptions
    private void MinigameAskPlayUI_OnMinigamePlay(object sender, System.EventArgs e)
    {
        mingamePlayTrigger = true;
    }

    private void FishingUI_OnFishingSuccess(object sender, System.EventArgs e)
    {
        fishingSuccess = true;
    }

    private void FishingUI_OnFishingFail(object sender, System.EventArgs e)
    {
        fishingFail = true;
    }

    #endregion
}
