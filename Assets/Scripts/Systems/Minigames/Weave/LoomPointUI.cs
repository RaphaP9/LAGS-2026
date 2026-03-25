using TMPro;
using UnityEngine;

public class LoomPointUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI numberText;

    [Header("Runtime Filled")]
    [SerializeField] private int pointIndex;
    [SerializeField] private Vector2 relativePosition;
    [Space]
    [SerializeField] private bool isWoven;
    [SerializeField] private int wovenAtIndex;

    public void SetLoomPointUI(RectTransform canvasRectTransform)
    {

    }
}
