using System.Collections;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField,Range(0f,5f)] private float startingMinigameTime;

    [Header("Runtime Filled")]
    [SerializeField] private State state;

    public enum State { StartingMinigame, AskForEnergy, WaitingForFish, PullingFishingRod, Success, Failure}

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
    }

    private void SetState(State state) => this.state = state;
}
