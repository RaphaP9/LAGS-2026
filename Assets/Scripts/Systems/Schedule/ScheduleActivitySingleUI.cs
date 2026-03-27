using TMPro;
using UnityEngine;

public class ScheduleActivitySingleUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI performedText;
    [SerializeField] private TextMeshProUGUI separatorText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Settings")]
    [SerializeField] private Color doneColor;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color notDoneColor;

    public void SetUI(ActivitySchedulePerformed activitySchedulePerformed)
    {
        totalText.text = activitySchedulePerformed.times.ToString();
        descriptionText.text = activitySchedulePerformed.description.ToString();

        bool done = activitySchedulePerformed.timesPerformed >= activitySchedulePerformed.times;

        if (done)
        {
            performedText.text = activitySchedulePerformed.times.ToString();

            performedText.color = doneColor;
            separatorText.color = doneColor;
            totalText.color = doneColor;
        }
        else
        {
            performedText.text = activitySchedulePerformed.timesPerformed.ToString();

            performedText.color = neutralColor;
            separatorText.color = notDoneColor;
            totalText.color = notDoneColor;
        }
    }
}
