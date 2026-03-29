using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections.Generic;

public class IntroductionManager : MonoBehaviour
{
    public static IntroductionManager Instance {  get; private set; }

    [Header("Enabler")]
    [SerializeField] private bool enableIntroduction;

    [Header("Components")]
    [SerializeField] private CinemachineCamera playerFollowCamera;
    [SerializeField] private CinemachineCamera introductionCamera;
    [Space]
    [SerializeField] private Animator introductionCameraAnimator;
    [Space]
    [SerializeField] private DialogueSO introductionDialogue;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float timeToStartIntroduction;
    [SerializeField] private List<SentenceIDAnimationTriggerRelation> sentenceIDAnimationTriggerRelations;

    public static event EventHandler OnIntroductionStart;
    public static event EventHandler OnIntroductionEnd;

    private const int HIGH_PRIORITY = 1;
    private const int LOW_PRIORITY = 0;

    private const string NULL_STRING = "";

    private bool introductionDialogueEnd = false;

    [System.Serializable]
    public class SentenceIDAnimationTriggerRelation
    {
        public int sentenceID;
        public string animationTrigger;
    }

    private void OnEnable()
    {
        DialogueManager.OnSentenceIdle += DialogueManager_OnSentenceIdle;
        DialogueManager.OnDialogueEnd += DialogueManager_OnDialogueEnd;
    }

    private void OnDisable()
    {
        DialogueManager.OnSentenceIdle -= DialogueManager_OnSentenceIdle;
        DialogueManager.OnDialogueEnd -= DialogueManager_OnDialogueEnd;
    }

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
        if (!enableIntroduction) return;
        if (StaticDataManager.Instance.Data.hasIntroducted) return;

        StartCoroutine(IntroductionCoroutine());

    }

    private IEnumerator IntroductionCoroutine()
    {
        OnIntroductionStart?.Invoke(this, EventArgs.Empty);
        GivePriorityToIntroductionCamera();

        yield return new WaitForSeconds(timeToStartIntroduction);

        DialogueManager.Instance.StartDialogue(introductionDialogue);

        yield return new WaitUntil(() => introductionDialogueEnd);

        introductionDialogueEnd = false;

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

    private void SetTriggerBySentenceAnimation(int sentenceID)
    {
        string animationTrigger = GetAnimationTriggerBySentenceID(sentenceID);
        if (animationTrigger == NULL_STRING) return;

        ResetAllTriggers();
        introductionCameraAnimator.SetTrigger(animationTrigger);
    }

    private string GetAnimationTriggerBySentenceID(int sentenceID)
    {
        foreach (SentenceIDAnimationTriggerRelation relation in sentenceIDAnimationTriggerRelations)
        {
            if (relation.sentenceID == sentenceID) return relation.animationTrigger;
        }

        return NULL_STRING;
    }

    private void ResetAllTriggers()
    {
        foreach(SentenceIDAnimationTriggerRelation relation in sentenceIDAnimationTriggerRelations)
        {
            introductionCameraAnimator.ResetTrigger(relation.animationTrigger);
        }
    }

    #region Subscriptions
    private void DialogueManager_OnSentenceIdle(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        if (e.dialogueSO != introductionDialogue) return;
        SetTriggerBySentenceAnimation(e.dialogueSentence.localID);
    }

    private void DialogueManager_OnDialogueEnd(object sender, DialogueManager.OnDialogueEventArgs e)
    {
        if (e.dialogueSO != introductionDialogue) return;
        introductionDialogueEnd = true; 
    }
    #endregion

}
