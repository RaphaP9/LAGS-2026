using System.Collections;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MinigameEnergyAskUI minigameEnergyAskUI;

    [Header("Settings")]
    [SerializeField, Range(0, 5)] private int fishingEnergyCost;

    [SerializeField,Range(0f,5f)] private float startingMinigameTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public enum State { StartingMinigame, AskForEnergy, WaitingForFish, PullingFishingRod, Success, Failure}

    private bool energySpent = false;

    private void OnEnable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI += MinigameEnergyAskUI_OnEnergySpentInUI;
    }

    private void OnDisable()
    {
        minigameEnergyAskUI.OnEnergySpentInUI -= MinigameEnergyAskUI_OnEnergySpentInUI;
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

        SetState(State.AskForEnergy);

        minigameEnergyAskUI.ShowUI(fishingEnergyCost);

        yield return new WaitUntil(() => energySpent);
        energySpent = false;

        minigameEnergyAskUI.HideUI();

        SetState(State.WaitingForFish);

    }

    private void SetState(State state) => this.state = state;

    #region Subscriptions
    private void MinigameEnergyAskUI_OnEnergySpentInUI(object sender, MinigameEnergyAskUI.OnEnergySpentInUIEventArgs e)
    {
        energySpent = true;
    }

    #endregion
}
