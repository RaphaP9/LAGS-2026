using UnityEngine;
using UnityEngine.UI;

public class MoodUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;
    [SerializeField] private Image moodFill;

    [Header("Settings")]
    [SerializeField, Range(0.01f,100f)] private float smoothLerpFactor;
    [SerializeField] private Color maxMoodColor;
    [SerializeField] private Color minMoodColor;

    [Header("Runtime Filled")]
    [SerializeField] private float targetFill;
    [SerializeField] private Color targetColor;

    private const float FILL_THRESHOLD = 0.05f;

    private void OnEnable()
    {
        MoodManager.OnMoodInitialized += MoodManager_OnMoodInitialized;
        MoodManager.OnMoodChanged += MoodManager_OnMoodChanged;
    }

    private void OnDisable()
    {
        MoodManager.OnMoodInitialized -= MoodManager_OnMoodInitialized;
        MoodManager.OnMoodChanged -= MoodManager_OnMoodChanged;
    }

    private void Update()
    {
        HandleMoodLerping();
    }

    private void HandleMoodLerping()
    {
        if(Mathf.Abs(targetFill - moodFill.fillAmount) < FILL_THRESHOLD) return;

        moodFill.fillAmount = Mathf.Lerp(moodFill.fillAmount, targetFill, smoothLerpFactor * Time.deltaTime);
        moodFill.color = Color.Lerp(moodFill.color, targetColor, smoothLerpFactor * Time.deltaTime);
    }


    private float NormalizeMood(int mood)
    {
        float normalizedMood = Mathf.InverseLerp(gameSettingsSO.minMood, gameSettingsSO.maxMood, mood);
        return normalizedMood;
    }

    private Color GetFillColor(int mood)
    {
        Color color = Color.Lerp(minMoodColor, maxMoodColor, NormalizeMood(mood));
        return color;
    }

    #region  Subscriptions
    private void MoodManager_OnMoodInitialized(object sender, MoodManager.OnMoodEventArgs e)
    {
        targetFill = NormalizeMood(e.mood);
        targetColor = GetFillColor(e.mood);

        moodFill.fillAmount = targetFill; //Instant
        moodFill.color = targetColor;
    }

    private void MoodManager_OnMoodChanged(object sender, MoodManager.OnMoodEventArgs e)
    {
        targetFill = NormalizeMood(e.mood);
        targetColor = GetFillColor(e.mood);
    }
    #endregion
}
