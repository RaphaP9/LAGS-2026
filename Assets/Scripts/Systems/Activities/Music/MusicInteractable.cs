using System;
using UnityEngine;

public class MusicInteractable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField, Range(1f, 100f)] private float interactionRange;
    [Space]
    [SerializeField] private string tooltipMessage;
    [Space]
    [SerializeField] private string minigameScene;
    [SerializeField] private TransitionType minigameSceneTransitionType;

    [Header("Time Settings")]
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    [Space]
    [SerializeField] private string message;
    [SerializeField] private Transform messageTransform;
    [SerializeField] private Color messageColor;
    [SerializeField] private float messageDuration;

    #region IInteractable Properties
    public float InteractionRange => interactionRange;
    public bool IsSelectable => GameManager.Instance.GameState == GameManager.State.Exploring;
    public string TooltipMessage => tooltipMessage;
    #endregion

    #region IInteractableEvents
    public event EventHandler OnInteractableSelected;
    public event EventHandler OnInteractableDeselected;
    public event EventHandler OnInteractableInteracted;
    #endregion

    #region  IInteractable Methods

    public void Interact()
    {
        if (DayTimeManager.Instance.IsCurrentTimeBetweenTwoTimes(minTime, maxTime))
        {
            ScenesManager.Instance.TransitionLoadTargetScene(minigameScene, minigameSceneTransitionType);
            PlayerPositionHandler.Instance.SavePlayerPosition();

        }
        else
        {
            MessageManager.Instance.CreateMessage(message, messageColor, messageTransform.position, messageDuration);
        }

        OnInteractableInteracted?.Invoke(this, EventArgs.Empty);
    }

    public void Select()
    {
        OnInteractableSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        OnInteractableDeselected?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetTransform() => transform;
    #endregion
}
