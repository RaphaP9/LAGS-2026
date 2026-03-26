using UnityEngine;
using System;
using System.Collections.Generic;

public class ActivitiesManager : MonoBehaviour
{
    public static ActivitiesManager Instance {  get; private set; }

    [Header("Fishing")]
    [SerializeField] private ActivitySO fishingSO;
    [SerializeField] private InventoryObjectSO fishSO;
    
    [Header("Harvesting")]
    [SerializeField] private ActivitySO harvestingSO;
    [SerializeField] private InventoryObjectSO totoraSO;

    [Header("Weaving")]
    [SerializeField] private ActivitySO weavingSO;

    [Header("Lists")]
    [SerializeField] private List<ActivitySO> activitiesPerformed;

    public static event EventHandler<OnActivitiesEventArgs> OnActivitiesPerformedInitialized;

    public static event EventHandler<OnActivityPerformedEventArgs> OnActivityPerformed;
    public static event EventHandler<OnActivityPerformedEventArgs> OnActivityPerformedSuccess;
    public static event EventHandler<OnActivityPerformedEventArgs> OnActivityPerformedFail;

    public class OnActivitiesEventArgs : EventArgs
    {
        public List<ActivitySO> activities;
    }

    public class OnActivityPerformedEventArgs : EventArgs
    {
        public ActivitySO activitySO;
        public List<ActivitySO> activitiesPerformed;
    }

    private void OnEnable()
    {
        FishingManager.OnFishingSuccess += FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval += FishingManager_OnFishingInterval;
        FishingManager.OnFishingIntervalSuccess += FishingManager_OnFishingIntervalSuccess;
        FishingManager.OnFishingIntervalFail += FishingManager_OnFishingIntervalFail;

        WeavingManager.OnWeaveInterval += WeaveManager_OnWeaveInterval;
        WeavingManager.OnWeaveIntervalSuccess += WeavingManager_OnWeaveIntervalSuccess;
        WeavingManager.OnWeaveIntervalFail += WeavingManager_OnWeaveIntervalFail;

        TotoraCropHandler.OnAnyTotoraCropHarvested += TotoraCropHandler_OnAnyTotoraCropHarvested;
    }

    private void OnDisable()
    {
        FishingManager.OnFishingSuccess -= FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval -= FishingManager_OnFishingInterval;
        FishingManager.OnFishingIntervalSuccess -= FishingManager_OnFishingIntervalSuccess;
        FishingManager.OnFishingIntervalFail -= FishingManager_OnFishingIntervalFail;

        WeavingManager.OnWeaveInterval -= WeaveManager_OnWeaveInterval;
        WeavingManager.OnWeaveIntervalSuccess -= WeavingManager_OnWeaveIntervalSuccess;
        WeavingManager.OnWeaveIntervalFail -= WeavingManager_OnWeaveIntervalFail;

        TotoraCropHandler.OnAnyTotoraCropHarvested -= TotoraCropHandler_OnAnyTotoraCropHarvested;
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
            //Debug.LogWarning("There is more than one ActivitiesManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeActivitiesPerformed();
    }

    private void InitializeActivitiesPerformed()
    {
        activitiesPerformed = new List<ActivitySO>(StaticDataManager.Instance.Data.currentActivitiesPerformed);

        OnActivitiesPerformedInitialized?.Invoke(this, new OnActivitiesEventArgs { activities = activitiesPerformed });
    }

    private void PerformActivity(ActivitySO activitySO)
    {
        activitiesPerformed.Add(activitySO);

        StaticDataManager.Instance.AddActivityPerformed(activitySO);

        OnActivityPerformed?.Invoke(this, new OnActivityPerformedEventArgs { activitySO = activitySO, activitiesPerformed = activitiesPerformed });
    }

    private void PerformActivitySuccess(ActivitySO activitySO)
    {
        OnActivityPerformedSuccess?.Invoke(this, new OnActivityPerformedEventArgs { activitySO = activitySO, activitiesPerformed = activitiesPerformed });
    }

    private void PerformActivityFail(ActivitySO activitySO)
    {
        OnActivityPerformedFail?.Invoke(this, new OnActivityPerformedEventArgs { activitySO = activitySO, activitiesPerformed = activitiesPerformed });
    }

    public void ResetActivitiesPerformed()
    {
        activitiesPerformed.Clear();
        StaticDataManager.Instance.ResetActivitiesPerformed();
    }

    #region Fishing Subscriptions
    private void FishingManager_OnFishingSuccess(object sender, System.EventArgs e)
    {
        InventoryManager.Instance.AddInventoryObject(fishSO, 1);
    }
    private void FishingManager_OnFishingInterval(object sender, System.EventArgs e)
    {
        PerformActivity(fishingSO);
    }
    private void FishingManager_OnFishingIntervalSuccess(object sender, EventArgs e)
    {
        PerformActivitySuccess(fishingSO);
    }

    private void FishingManager_OnFishingIntervalFail(object sender, EventArgs e)
    {
        PerformActivityFail(fishingSO);
    }
    #endregion

    #region Weaving Subscriptions
    private void WeaveManager_OnWeaveInterval(object sender, System.EventArgs e)
    {
        PerformActivity(weavingSO);
    }
    private void WeavingManager_OnWeaveIntervalSuccess(object sender, EventArgs e)
    {
        PerformActivitySuccess(weavingSO);
    }
    private void WeavingManager_OnWeaveIntervalFail(object sender, EventArgs e)
    {
        PerformActivityFail(weavingSO);
    }

    #endregion

    #region Harvesting Subscriptions
    private void TotoraCropHandler_OnAnyTotoraCropHarvested(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        InventoryManager.Instance.AddInventoryObject(totoraSO, 1);
        PerformActivity(harvestingSO);
        PerformActivitySuccess(harvestingSO);
    }
    #endregion
}
