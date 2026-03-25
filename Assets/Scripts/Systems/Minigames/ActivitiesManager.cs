using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    [Header("Fishing")]
    [SerializeField] private InventoryObjectSO fishSO;
    [SerializeField, Range(0,100)] private int fishingEnergyCost;
    [SerializeField, Range(0, 600)] private int timeAddPerFishing;

    [Header("Weaving")]
    [SerializeField, Range(0, 100)] private int weavingEnergyCost;
    [SerializeField, Range(0, 600)] private int timeAddPerWeave;

    public int FishingEnergyCost => fishingEnergyCost;
    public int WeavingEnergyCost => weavingEnergyCost;

    private void OnEnable()
    {
        FishingManager.OnFishingSuccess += FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval += FishingManager_OnFishingInterval;

        WeavingManager.OnWeaveInterval += WeaveManager_OnWeaveInterval;   
    }

    private void OnDisable()
    {
        FishingManager.OnFishingSuccess -= FishingManager_OnFishingSuccess;
        FishingManager.OnFishingInterval -= FishingManager_OnFishingInterval;

        WeavingManager.OnWeaveInterval -= WeaveManager_OnWeaveInterval;
    }

    #region Fishing Subscriptions
    private void FishingManager_OnFishingSuccess(object sender, System.EventArgs e)
    {
        InventoryManager.Instance.AddInventoryObject(fishSO, 1);
    }
    private void FishingManager_OnFishingInterval(object sender, System.EventArgs e)
    {
        DayTimeManager.Instance.AddTime(timeAddPerFishing);
    }
    #endregion

    #region Weaving Subscriptions
    private void WeaveManager_OnWeaveInterval(object sender, System.EventArgs e)
    {
        DayTimeManager.Instance.AddTime(timeAddPerWeave);

    }
    #endregion
}
