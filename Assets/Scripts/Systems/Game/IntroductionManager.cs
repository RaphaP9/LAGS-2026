using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{
    public static IntroductionManager Instance {  get; private set; }

    [Header("Components")]
    [SerializeField] private CinemachineCamera playerFollowCamera;
    [SerializeField] private CinemachineCamera introductionCamera;
    [Space]
    [SerializeField] private Animator introductionAnimator;
    [Space]
    [SerializeField] private DialogueSO introductionDialogue;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float timeToStartIntroduction;

    public static event EventHandler OnIntroductionStart;
    public static event EventHandler OnIntroductionEnd;

    private const int HIGH_PRIORITY = 1;
    private const int LOW_PRIORITY = 0;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one IntroductionManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HandleIntroduction();
    }

    private void HandleIntroduction()
    {
        if (StaticDataManager.Instance.Data.hasIntroducted) return;

        StartCoroutine(IntroductionCoroutine());

    }

    private IEnumerator IntroductionCoroutine()
    {
        OnIntroductionStart?.Invoke(this, EventArgs.Empty);
        GivePriorityToIntroductionCamera();

        yield return new WaitForSeconds(timeToStartIntroduction);

        OnIntroductionEnd?.Invoke(this, EventArgs.Empty);
        GivePriorityToPlayerFollorCamera();

        StaticDataManager.Instance.SetHasIntroductedTrue();
    }

    private void GivePriorityToIntroductionCamera()
    {
        introductionCamera.Priority = HIGH_PRIORITY;
        playerFollowCamera.Priority = LOW_PRIORITY;
    }

    private void GivePriorityToPlayerFollorCamera()
    {
        introductionCamera.Priority = LOW_PRIORITY;
        playerFollowCamera.Priority = HIGH_PRIORITY;
    }
}
