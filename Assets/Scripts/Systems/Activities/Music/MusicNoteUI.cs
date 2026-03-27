using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicNoteUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MusicNoteSO musicNoteSO;
    [Space]
    [SerializeField] private MusicMinigameUI musicMinigameUI;
    [SerializeField] private Button noteButton;
    [SerializeField] private TextMeshProUGUI nameText;

    public static event EventHandler<OnNoteEventArgs> OnNotePlayed;

    public class OnNoteEventArgs : EventArgs
    {
        public MusicNoteSO musicNoteSO;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void Start()
    {
        InitializeNote();
    }

    private void InitializeButtonsListeners()
    {
        noteButton.onClick.AddListener(PlayNote);
    }

    private void InitializeNote()
    {
        nameText.text = musicNoteSO.noteName;
    }

    private void PlayNote()
    {
        if (!musicMinigameUI.CanPlayNote()) return;
        OnNotePlayed?.Invoke(this, new OnNoteEventArgs { musicNoteSO = musicNoteSO });
    }
}
