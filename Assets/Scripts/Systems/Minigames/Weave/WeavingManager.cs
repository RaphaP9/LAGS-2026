using System;
using System.Collections;
using UnityEngine;

public class WeavingManager : MonoBehaviour
{
    public static WeavingManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private ActivitiesManager activitiesManager;
    [SerializeField] private MinigameEnergyAskUI minigameEnergyAskUI;
    [SerializeField] private WeavingUI weaveUI;

    [Header("Settings")]
    [SerializeField, Range(0f, 5f)] private float startingMinigameTime;
    [SerializeField, Range(0f, 5f)] private float waitForLoomTime;
    [SerializeField, Range(0f, 5f)] private float minigameIntervalTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public static event EventHandler OnWaitForLoom;
    public static event EventHandler OnWeaving;
    public static event EventHandler OnWeaveSuccess;
    public static event EventHandler OnWeaveFail;
    public static event EventHandler OnWeaveInterval;

    public enum State { StartingMinigame, AskForEnergy, WaitingForLoom, Weaving, MinigameInterval }

    private bool energySpent = false;
    private bool weaveSuccess = false;
    private bool weaveFail = false;

    private void OnEnable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI += MinigameEnergyAskUI_OnEnergySpentInUI;
        weaveUI.OnWeaveSuccess += WeaveUI_OnWeaveSuccess;
        weaveUI.OnWeaveFail += WeaveUI_OnWeaveFail;
    }

    private void OnDisable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI -= MinigameEnergyAskUI_OnEnergySpentInUI;
        weaveUI.OnWeaveSuccess -= WeaveUI_OnWeaveSuccess;
        weaveUI.OnWeaveFail -= WeaveUI_OnWeaveFail;
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

            minigameEnergyAskUI.ShowUI(activitiesManager.WeavingEnergyCost);

            yield return new WaitUntil(() => energySpent);
            energySpent = false;

            minigameEnergyAskUI.HideUI();

            SetState(State.WaitingForLoom);

            OnWaitForLoom?.Invoke(this, EventArgs.Empty);

            yield return new WaitForSeconds(waitForLoomTime);

            SetState(State.Weaving);

            OnWeaving?.Invoke(this, EventArgs.Empty);

            weaveUI.StartWeavingGame();

            yield return new WaitUntil(() => weaveSuccess || weaveFail);

            if (weaveSuccess) OnWeaveSuccess?.Invoke(this, EventArgs.Empty);

            if (weaveFail) OnWeaveFail?.Invoke(this, EventArgs.Empty);

            weaveSuccess = false;
            weaveFail = false;

            SetState(State.MinigameInterval);

            yield return new WaitForSeconds(minigameIntervalTime);

            OnWeaveInterval?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetState(State state) => this.state = state;

    #region Subscriptions
    private void MinigameEnergyAskUI_OnEnergySpentInUI(object sender, MinigameEnergyAskUI.OnEnergySpentInUIEventArgs e)
    {
        energySpent = true;
    }
    private void WeaveUI_OnWeaveSuccess(object sender, System.EventArgs e)
    {
        weaveSuccess = true;
    }

    private void WeaveUI_OnWeaveFail(object sender, System.EventArgs e)
    {
        weaveFail = true;
    }

    #endregion
}
