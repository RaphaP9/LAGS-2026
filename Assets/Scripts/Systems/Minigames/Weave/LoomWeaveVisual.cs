using UnityEngine;

public class LoomWeaveVisual : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UILineRenderer UILineRenderer;

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

    private void LoomPointUI_OnPointWoven(object sender, LoomPointUI.OnPointWovenEventArgs e)
    {
        AddUILineRendererPoint(e.relativePosition);
    }
}
