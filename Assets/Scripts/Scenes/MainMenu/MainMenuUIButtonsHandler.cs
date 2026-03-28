using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIButtonsHandler : MonoBehaviour
{
    [Header("Continue")]
    [SerializeField] private Button playButton;
    [SerializeField] private TransitionType playTransitionType;
    [SerializeField] private string playScene;

    [Header("Credits")]
    [SerializeField] private Button creditsButton;
    [SerializeField] private TransitionType creditsTransitionType;
    [SerializeField] private string creditsScene;

    [Header("Other")]
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        playButton.onClick.AddListener(LoadPlayScene);
        creditsButton.onClick.AddListener(LoadCreditsScene);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void LoadPlayScene() => ScenesManager.Instance.TransitionLoadTargetScene(playScene, playTransitionType);
    private void LoadCreditsScene() => ScenesManager.Instance.TransitionLoadTargetScene(creditsScene, creditsTransitionType);
    private void QuitGame() => ScenesManager.Instance.QuitGame();
}
