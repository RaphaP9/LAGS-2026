using UnityEngine;
using UnityEngine.UI;

public class SewUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image image;

    [Header("Runtime Filled")]
    [SerializeField] private LoomPointUI linkedLoomPointUI;

    public LoomPointUI LinkedLoomPointUI => linkedLoomPointUI;

    public void SetSew(LoomPointUI loomPointUI, Color color )
    {
        linkedLoomPointUI = loomPointUI;
        image.color = color;
    }

    public void DestroySew()
    {
        Destroy(gameObject);
    }
}
