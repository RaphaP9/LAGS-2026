using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoomPointUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI numberText;

    [Header("Runtime Filled")]
    [SerializeField] private int pointNumber;
    [SerializeField] private Vector2 relativePosition;
    [Space]
    [SerializeField] private bool isWoven;
    [SerializeField] private int wovenAtNumber;

    private RectTransform refferenceRectTransform;
    private WeavingUI weavingUI;

    public static event EventHandler<OnPointWovenEventArgs> OnPointWoven;

    public class OnPointWovenEventArgs : EventArgs
    {
        public LoomPointUI loomPointUI;
        public int pointNumber;
        public Vector2 relativePosition;
    }

    public void SetLoomPointUI(int pointNumber, RectTransform refferenceRectTransform, WeavingUI weavingUI)
    {
        this.pointNumber = pointNumber;
        this.refferenceRectTransform = refferenceRectTransform;
        this.weavingUI = weavingUI;

        isWoven = false;
        numberText.text = pointNumber.ToString();

        Vector3 worldPos = transform.position;
        relativePosition = refferenceRectTransform.InverseTransformPoint(worldPos);
    }

    private void WeavePoint()
    {
        isWoven = true;
        OnPointWoven?.Invoke(this, new OnPointWovenEventArgs {loomPointUI = this, pointNumber = pointNumber, relativePosition = relativePosition });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!weavingUI.CanWeave()) return;

        if (isWoven) return;
        WeavePoint();       
    }
}
