using UnityEngine;

public class FisherNPCInteractable : DialogueNPCInteractable
{
    protected override void IncreaseTimesInteractedNPC()
    {
        StaticDataManager.Instance.IncreaseTimesInteractedFisher();
    }

    protected override bool IsFirstTimeInteracting()
    {
        if (StaticDataManager.Instance.Data.timesInteractedFisher <= 0) return true;
        return false;
    }
}
