using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoodUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;
    [SerializeField] private Image moodFill;
    [SerializeField] private Slider moodSlider;
    [SerializeField] private Image moodImage;

    [Header("Settings")]
    [SerializeField, Range(0.01f,100f)] private float smoothLerpFactor;
    [SerializeField] private Color maxMoodColor;
    [SerializeField] private Color minMoodColor;

    [Header("Images")]
    [SerializeField] private List<SpriteMood> spriteMoodList; //Note, put on descendent time order

    [Header("Runtime Filled")]
    [SerializeField] private float targetValue;
    [SerializeField] private Color targetColor;

    private const float FILL_THRESHOLD = 0.05f;

    [System.Serializable]
    public class SpriteMood
    {
        [Range(0, 100)] public int minMood;
        public Sprite sprite;
    }

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
        if(Mathf.Abs(targetValue - moodSlider.value) < FILL_THRESHOLD) return;

        moodSlider.value = Mathf.Lerp(moodSlider.value, targetValue, smoothLerpFactor * Time.deltaTime);
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

    private Sprite GetMoodSprite(int mood)
    {
        foreach(SpriteMood spriteMood in spriteMoodList)
        {
            if(mood >= spriteMood.minMood) return spriteMood.sprite;
        }

        return spriteMoodList[^1].sprite;
    }

    #region  Subscriptions
    private void MoodManager_OnMoodInitialized(object sender, MoodManager.OnMoodEventArgs e)
    {
        targetValue = NormalizeMood(e.mood);
        targetColor = GetFillColor(e.mood);

        moodSlider.value = targetValue; //Instant
        moodFill.color = targetColor;
        moodImage.sprite = GetMoodSprite(e.mood);
    }

    private void MoodManager_OnMoodChanged(object sender, MoodManager.OnMoodEventArgs e)
    {
        targetValue = NormalizeMood(e.mood);
        targetColor = GetFillColor(e.mood);
        moodImage.sprite = GetMoodSprite(e.mood);
    }
    #endregion
}
