using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI dayText;

    private void OnEnable()
    {
        DayTimeManager.OnDayInitialized += DayTimeManager_OnDayInitialized;
    }

    private void OnDisable()
    {
        DayTimeManager.OnDayInitialized -= DayTimeManager_OnDayInitialized;
    }

    private void SetDayText(int day) => dayText.text = day.ToString();

    private void DayTimeManager_OnDayInitialized(object sender, DayTimeManager.OnDayEventArgs e)
    {
        SetDayText(e.day);  
    }
}
