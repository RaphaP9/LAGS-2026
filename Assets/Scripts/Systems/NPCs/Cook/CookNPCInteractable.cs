using UnityEngine;

public class CookNPCInteractable : DialogueNPCInteractable
{
    protected override void IncreaseTimesInteractedNPC()
    {
        StaticDataManager.Instance.IncreaseTimesInteractedCook();
    }

    protected override bool IsFirstTimeInteracting()
    {
        if (StaticDataManager.Instance.Data.timesInteractedCook <= 0) return true;
        return false;
    }
}
