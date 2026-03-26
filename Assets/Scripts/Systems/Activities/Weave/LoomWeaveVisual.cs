using UnityEngine;

public class LoomWeaveVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UILineRenderer UILineRenderer;
    [SerializeField] private Transform sewPrefab;

    private void OnEnable()
    {
        LoomPointUI.OnPointWoven += LoomPointUI_OnPointWoven;
    }
    private void OnDisable()
    {
        LoomPointUI.OnPointWoven -= LoomPointUI_OnPointWoven;
    }

    private void AddUILineRendererPoint(Vector2 position)
    {
        UILineRenderer.AddPoint(position);
    }

    private void AddSewPoint(Vector2 position)
    {
        Transform sewTransform = Instantiate(sewPrefab, transform);
        RectTransform rectTransorm = sewTransform.GetComponent<RectTransform>();
        rectTransorm.localPosition = position;
    }

    private void LoomPointUI_OnPointWoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        AddSewPoint(e.relativePosition);
        AddUILineRendererPoint(e.relativePosition);
    }
}
