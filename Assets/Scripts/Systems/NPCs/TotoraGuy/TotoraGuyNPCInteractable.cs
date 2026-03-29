using UnityEngine;

public class TotoraGuyNPCInteractable : DialogueNPCInteractable
{
    protected override void IncreaseTimesInteractedNPC()
    {
        StaticDataManager.Instance.IncreaseTimesInteractedTotoraGuy();
    }

    protected override bool IsFirstTimeInteracting()
    {
        if (StaticDataManager.Instance.Data.timesInteractedTotoraGuy <= 0) return true;
        return false;
    }
}
