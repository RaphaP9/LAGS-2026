using System.Collections;
using TMPro;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Runtime Filled")]
    [SerializeField] private float duration;

    private const float TIME_TO_DESTROY_AFTER_HIDE = 1f;
    private const string HIDE_TRIGER = "Hide";

    public void SetMessage(Camera camera, string message, Color messageColor, Vector3 position, float duration)
    {
        canvas.worldCamera = camera;
        messageText.text = message;
        messageText.color = messageColor;

        transform.position = position;
        this.duration = duration;

        StartCoroutine(MessageCoroutine());
    }

    private IEnumerator MessageCoroutine()
    {
        yield return new WaitForSeconds(duration);
        HideMessage();
        yield return new WaitForSeconds(TIME_TO_DESTROY_AFTER_HIDE);
        DestroyGameObject();
    }

    private void HideMessage()
    {
        animator.SetTrigger(HIDE_TRIGER);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
