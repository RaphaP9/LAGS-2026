using UnityEngine;
using UnityEngine.UI;

public class SewUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image image;

    public void SetColor(Color color) => image.color = color;
}
