using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicNoteUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MusicMinigameUI musicMinigameUI;
    [SerializeField] private MusicNoteSO musicNoteSO;
    [SerializeField] private Button noteButton;

    public static event EventHandler<OnNoteEventArgs> OnNotePlayed;

    public class OnNoteEventArgs : EventArgs
    {
        public MusicNoteSO musicNoteSO;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        noteButton.onClick.AddListener(PlayNote);
    }

    private void PlayNote()
    {
        if (!musicMinigameUI.CanPlayNote()) return;
        OnNotePlayed?.Invoke(this, new OnNoteEventArgs { musicNoteSO = musicNoteSO });
    }
}
