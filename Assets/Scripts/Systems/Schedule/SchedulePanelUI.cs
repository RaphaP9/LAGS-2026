using UnityEngine;
using System.Collections.Generic;

public class SchedulePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform activitiesContainer;
    [SerializeField] private Transform activityPrefab;

    private void OnEnable()
    {
        ScheduleManager.OnScheduleManagerInitialized += ScheduleManager_OnScheduleManagerInitialized;
        ScheduleManager.OnScheduleChange += ScheduleManager_OnScheduleChange;
    }

    private void OnDisable()
    {
        ScheduleManager.OnScheduleManagerInitialized -= ScheduleManager_OnScheduleManagerInitialized;
        ScheduleManager.OnScheduleChange -= ScheduleManager_OnScheduleChange;
    }

    private void UpdateUI(List<ActivitySchedulePerformed> activitiesSchedulePerformed)
    {
        ClearContainer();

        foreach(ActivitySchedulePerformed activitySchedulePerformed in activitiesSchedulePerformed)
        {
            CreateSinglePrefab(activitySchedulePerformed);
        }
    }

    private void ClearContainer()
    {
        foreach(Transform child in activitiesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateSinglePrefab(ActivitySchedulePerformed activitySchedulePerformed)
    {
        Transform prefabTransform = Instantiate(activityPrefab, activitiesContainer);

        ScheduleActivitySingleUI scheduleActivitySingleUI = prefabTransform.GetComponent<ScheduleActivitySingleUI>();

        if (scheduleActivitySingleUI == null) return;

        scheduleActivitySingleUI.SetUI(activitySchedulePerformed);
    }

    #region Subscriptions
    private void ScheduleManager_OnScheduleManagerInitialized(object sender, ScheduleManager.OnActivityScheduleEventArgs e)
    {
        UpdateUI(e.activitiesPerformedList);
    }

    private void ScheduleManager_OnScheduleChange(object sender, ScheduleManager.OnActivityScheduleEventArgs e)
    {
        UpdateUI(e.activitiesPerformedList);

    }
    #endregion
}
