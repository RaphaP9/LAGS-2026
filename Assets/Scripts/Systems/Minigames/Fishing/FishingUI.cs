using System;
using System.Collections;
using UnityEngine;

public class FishingUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private RectTransform fillHolder;
    [SerializeField] private RectTransform indicatorHolder;
    [SerializeField] private MinigamesInput minigamesInput;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float startingTime;
    [SerializeField, Range(0f, 3f)] private float endingTime;
    [Space]
    [SerializeField, Range(0f, 1f)] private float indicatorImpulse;
    [SerializeField, Range(0f, 1f)] private float indicatorBackMovePerSecond;
    [Space]
    [SerializeField, Range(0.01f, 100f)] private float fillTiltSmoothFactor;
    [SerializeField, Range(0.01f, 100f)] private float indicatorTiltSmoothFactor;
    [Space]
    [SerializeField, Range(0f, 1f)] private float tiltMinDifference;
    [SerializeField, Range(0f, 1f)] private float tiltMaxDifference;
    [Space]
    [SerializeField, Range(1, 10)] private int minTilts;
    [SerializeField, Range(1, 10)] private int maxTilts;
    [Space]
    [SerializeField, Range(0f, 1f)] private float safeZoneWidth;
    [Space]
    [SerializeField, Range(1f, 10f)] private float minSafeZoneStayTime; 
    [SerializeField, Range(1f, 10f)] private float maxSafeZoneStayTime;
    [Space]
    [SerializeField, Range(1f, 10f)] private float minOutStayTime;
    [SerializeField, Range(1f, 10f)] private float maxOutStayTime;
    [Space]
    [SerializeField, Range(0f, 1f)] private float minFillTilt;
    [SerializeField, Range(0f, 1f)] private float maxFillTilt;
    [Space]
    [SerializeField, Range(0f, 1f)] private float minIndicatorTilt;
    [SerializeField, Range(0f, 1f)] private float maxIndicatorTilt;
    [Space]
    [SerializeField] private float minPositionX;
    [SerializeField] private float maxPositionX;

    [Header("Runtime Filled")]
    [SerializeField] private State state;
    [SerializeField] private float currentFillTilt;
    [SerializeField] private float currentIndicatorTilt;

    public enum State {NotPlaying, Starting, Playing}

    private const float MIDDLE_TILT = 0.5f;

    private const string SHOW_TRIGGER = "Show";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";

    public event EventHandler OnFishingSuccess;
    public event EventHandler OnFishingFail;

    public static event EventHandler OnTiltSuccess;
    public static event EventHandler OnTiltFail;

    private void Update()
    {
        HandleLerpFillTilt();
        HandleIndicatorMovement();
    }

    public void StartFishingUIGame()
    {
        StartCoroutine(FishingUIGameCoroutine());   
    }

    private IEnumerator FishingUIGameCoroutine()
    {
        SetState(State.Starting);

        ResetPositionsInstantly();

        ShowUI();

        yield return new WaitForSeconds(startingTime);

        SetState(State.Playing);

        int remainingTilts = GeneralUtilities.GetRandomBetweenTwoInts(minTilts, maxTilts);

        Debug.Log(remainingTilts);

        for(int i = 0; i < remainingTilts; i++)
        {
            float safeZoneStayTime = GeneralUtilities.GetRandomBetweenTwoFloats(minSafeZoneStayTime, maxSafeZoneStayTime);
            float outStayTime = GeneralUtilities.GetRandomBetweenTwoFloats(minOutStayTime, maxOutStayTime);

            bool validTilt = false;
            float chosenTilt = 0f;

            while (!validTilt)
            {
                float potentialTilt = GeneralUtilities.GetRandomBetweenTwoFloats(minFillTilt, maxFillTilt);

                if (!GeneralUtilities.IsAtLeastDistance(potentialTilt, currentFillTilt, tiltMinDifference)) continue;
                if (!GeneralUtilities.IsAtMostDistance(potentialTilt, currentFillTilt, tiltMaxDifference)) continue;

                chosenTilt = potentialTilt;
                validTilt = true;
            }

            currentFillTilt = chosenTilt;

            float minSafeZoneTilt = currentFillTilt - safeZoneWidth / 2;
            float maxSafeZoneTilt = currentFillTilt + safeZoneWidth / 2;

            bool tiltSucceeded = false;
            float currentSafeZoneTime = 0f;
            float currentOutTime = 0f;

            while (!tiltSucceeded)
            {
                if(GeneralUtilities.IsBetween(minSafeZoneTilt, maxSafeZoneTilt, currentIndicatorTilt))
                {
                    currentSafeZoneTime += Time.deltaTime;
                    currentOutTime = 0f;
                }
                else
                {
                    currentOutTime += Time.deltaTime;
                    currentSafeZoneTime = 0f;
                }

                if (currentOutTime >= outStayTime)
                {
                    SetState(State.NotPlaying);

                    OnTiltFail?.Invoke(this, EventArgs.Empty);

                    yield return new WaitForSeconds(endingTime);
                    Fail();

                    yield break;
                }

                if(currentSafeZoneTime >= safeZoneStayTime)
                {
                    OnTiltSuccess?.Invoke(this, EventArgs.Empty);
                    tiltSucceeded = true;
                }

                yield return null;
            }
        }

        SetState(State.NotPlaying);

        yield return new WaitForSeconds(endingTime);
        Success();
    }

    private void ResetPositionsInstantly()
    {
        float centerPoint = (minPositionX + maxPositionX) / 2;

        fillHolder.anchoredPosition = new Vector2 (centerPoint, fillHolder.anchoredPosition.y);
        indicatorHolder.anchoredPosition = new Vector2 (centerPoint, indicatorHolder.anchoredPosition.y);

        currentFillTilt = MIDDLE_TILT;
        currentIndicatorTilt = MIDDLE_TILT;
    }

    private void HandleLerpFillTilt()
    {
        if (state != State.Playing) return;

        float xFillTargetPosition = Mathf.Lerp(minPositionX, maxPositionX, currentFillTilt);

        float xFillPosition = Mathf.Lerp(fillHolder.anchoredPosition.x, xFillTargetPosition, fillTiltSmoothFactor * Time.deltaTime);

        fillHolder.anchoredPosition = new Vector2(xFillPosition, fillHolder.anchoredPosition.y);
    }

    private void HandleIndicatorMovement()
    {
        if (state != State.Playing) return;

        currentIndicatorTilt = (currentIndicatorTilt - indicatorBackMovePerSecond * Time.deltaTime) < minIndicatorTilt? minIndicatorTilt : (currentIndicatorTilt - indicatorBackMovePerSecond * Time.deltaTime);

        if (minigamesInput.GetFishDown())
        {
            currentIndicatorTilt = currentIndicatorTilt + indicatorImpulse > maxIndicatorTilt? maxIndicatorTilt : currentIndicatorTilt + indicatorImpulse;
        }

        float xIndicatorTargetPosition = Mathf.Lerp(minPositionX, maxPositionX, currentIndicatorTilt);

        float xIndicatorPosition = Mathf.Lerp(indicatorHolder.anchoredPosition.x, xIndicatorTargetPosition, indicatorTiltSmoothFactor * Time.deltaTime);

        indicatorHolder.anchoredPosition = new Vector2(xIndicatorPosition, indicatorHolder.anchoredPosition.y);
    }

    private void Fail()
    {
        OnFishingFail?.Invoke(this, EventArgs.Empty);
        FailUI();
    }

    private void Success()
    {
        OnFishingSuccess?.Invoke(this, EventArgs.Empty);
        SuccessUI();
    }

    private void SetState(State state) => this.state = state;

    #region Animations
    public void ShowUI()
    {
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void SuccessUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    public void FailUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }
    #endregion
}
