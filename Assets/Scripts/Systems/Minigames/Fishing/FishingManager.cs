using System;
using System.Collections;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MinigameEnergyAskUI minigameEnergyAskUI;
    [SerializeField] private FishingUI fishingUI;

    [Header("Settings")]
    [SerializeField, Range(0, 5)] private int fishingEnergyCost;
    [Space]
    [SerializeField, Range(0f,5f)] private float startingMinigameTime;
    [SerializeField, Range(0f, 5f)] private float minWaitForFishTime;
    [SerializeField, Range(0f, 5f)] private float maxWaitForFishTime;
    [SerializeField, Range(0f, 5f)] private float minigameIntervalTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public static event EventHandler OnFishingSuccess;
    public static event EventHandler OnFishingFail;

    public enum State { StartingMinigame, AskForEnergy, WaitingForFish, PullingFishingRod, MinigameInterval}

    private bool energySpent = false;
    private bool fishingSuccess = false;
    private bool fishingFail = false;

    private void OnEnable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI += MinigameEnergyAskUI_OnEnergySpentInUI;
        fishingUI.OnFishingSuccess += FishingUI_OnFishingSuccess;
        fishingUI.OnFishingFail += FishingUI_OnFishingFail;
    }

    private void OnDisable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI -= MinigameEnergyAskUI_OnEnergySpentInUI;
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
            SetState(State.AskForEnergy);

            minigameEnergyAskUI.ShowUI(fishingEnergyCost);

            yield return new WaitUntil(() => energySpent);
            energySpent = false;

            minigameEnergyAskUI.HideUI();

            SetState(State.WaitingForFish);

            float timeToWait = GeneralUtilities.GetRandomBetweenTwoFloats(minWaitForFishTime, maxWaitForFishTime);

            yield return new WaitForSeconds(timeToWait);

            SetState(State.PullingFishingRod);

            fishingUI.StartFishingUIGame();

            yield return new WaitUntil(() => fishingSuccess || fishingFail);

            if(fishingSuccess) OnFishingSuccess?.Invoke(this, EventArgs.Empty);
            if(fishingFail) OnFishingFail?.Invoke(this,EventArgs.Empty);

            fishingSuccess = false;
            fishingFail = false;

            SetState(State.MinigameInterval);

            yield return new WaitForSeconds(minigameIntervalTime);

        }
    }

    private void SetState(State state) => this.state = state;

    #region Subscriptions
    private void MinigameEnergyAskUI_OnEnergySpentInUI(object sender, MinigameEnergyAskUI.OnEnergySpentInUIEventArgs e)
    {
        energySpent = true;
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
