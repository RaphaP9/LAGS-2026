using UnityEngine;
using System.Collections.Generic;
using System;

public class PartitionUI : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<PartitionNoteUI> partitionNotes;

    [Header("Runtime Filled")]
    [SerializeField] private List<MusicNoteSO> playedMusicNotes;

    public static event EventHandler OnPartitionSuccess;
    public static event EventHandler OnPartitionFail;

    private void OnEnable()
    {
        MusicNoteUI.OnNotePlayed += MusicNoteUI_OnNotePlayed;
    }

    private void OnDisable()
    {
        MusicNoteUI.OnNotePlayed -= MusicNoteUI_OnNotePlayed;
    }

    private void AddNoteToPlayedNotes(MusicNoteSO musicNoteSO)
    {
        playedMusicNotes.Add(musicNoteSO);

        if (playedMusicNotes.Count > partitionNotes.Count)
        {
            OnPartitionFail?.Invoke(this, EventArgs.Empty);
            return;
        }

        int index = playedMusicNotes.Count - 1;
        PartitionNoteUI partitionNoteOnIndex = partitionNotes[index];

        if(partitionNoteOnIndex.MusicNoteSO == musicNoteSO)
        {
            partitionNoteOnIndex.SuccessPartitionNote();
        }
        else
        {
            partitionNoteOnIndex.FailPartitionNote();
            OnPartitionFail?.Invoke(this, EventArgs.Empty);
            return;
        }

        if(HasCompletedPartition()) OnPartitionSuccess?.Invoke(this, EventArgs.Empty);
    }

    private bool HasCompletedPartition()
    {
        if (playedMusicNotes.Count >= partitionNotes.Count) return true; //Assuming all notes are correct, only check quantity
        return false;
    }

    #region Subscriptions
    private void MusicNoteUI_OnNotePlayed(object sender, MusicNoteUI.OnNoteEventArgs e)
    {
        AddNoteToPlayedNotes(e.musicNoteSO);
    }
    #endregion
}
