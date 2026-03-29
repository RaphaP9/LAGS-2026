using UnityEngine;

public class MusicianNPCInteractable : DialogueNPCInteractable
{
    protected override void IncreaseTimesInteractedNPC()
    {
        StaticDataManager.Instance.IncreaseTimesInteractedMusician();
    }

    protected override bool IsFirstTimeInteracting()
    {
        if (StaticDataManager.Instance.Data.timesInteractedMusician <= 0) return true;
        return false;
    }
}
