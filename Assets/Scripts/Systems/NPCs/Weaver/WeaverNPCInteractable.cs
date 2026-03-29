using UnityEngine;

public class WeaverNPCInteractable : DialogueNPCInteractable
{
    protected override void IncreaseTimesInteractedNPC()
    {
        StaticDataManager.Instance.IncreaseTimesInteractedWeaver();
    }

    protected override bool IsFirstTimeInteracting()
    {
        if (StaticDataManager.Instance.Data.timesInteractedWeaver <= 0) return true;
        return false;
    }
}
