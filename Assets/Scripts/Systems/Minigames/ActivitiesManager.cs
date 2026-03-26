using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    public static ActivitiesManager Instance {  get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Inventory")]
    [SerializeField] private InventoryObjectSO fishSO;
    [SerializeField] private InventoryObjectSO totoraSO;

    private void OnEnable()
    {
        FishingManager.OnFishingSuccess += FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval += FishingManager_OnFishingInterval;

        WeavingManager.OnWeaveInterval += WeaveManager_OnWeaveInterval;

        TotoraCropHandler.OnAnyTotoraCropHarvested += TotoraCropHandler_OnAnyTotoraCropHarvested;
    }

    private void OnDisable()
    {
        FishingManager.OnFishingSuccess -= FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval -= FishingManager_OnFishingInterval;

        WeavingManager.OnWeaveInterval -= WeaveManager_OnWeaveInterval;

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

    #region Fishing Subscriptions
    private void FishingManager_OnFishingSuccess(object sender, System.EventArgs e)
    {
        InventoryManager.Instance.AddInventoryObject(fishSO, 1);
    }
    private void FishingManager_OnFishingInterval(object sender, System.EventArgs e)
    {
        DayTimeManager.Instance.AddTime(gameSettingsSO.timeAddPerFishing);
    }
    #endregion

    #region Weaving Subscriptions
    private void WeaveManager_OnWeaveInterval(object sender, System.EventArgs e)
    {
        DayTimeManager.Instance.AddTime(gameSettingsSO.timeAddPerWeave);

    }
    #endregion

    #region Harvesting Subscriptions
    private void TotoraCropHandler_OnAnyTotoraCropHarvested(object sender, TotoraCropHandler.OnTotoraCropEventArgs e)
    {
        InventoryManager.Instance.AddInventoryObject(totoraSO, 1);
        DayTimeManager.Instance.AddTime(gameSettingsSO.timeAddPerHarvest);
    }
    #endregion
}
