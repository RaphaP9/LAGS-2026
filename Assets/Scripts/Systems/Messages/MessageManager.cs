using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private Transform defaultMessageTransform;
    [SerializeField] private Transform messagePrefab;
    [SerializeField] private Camera _camera;

    [Header("Settings")]
    [SerializeField, Range(0.1f,3f)] private float messageCooldown;
    [Space]
    [SerializeField] private Color defaultMessageColor;
    [SerializeField, Range(1f, 10f)] private float defaultMessageDuration;

    private float currentMessageCooldown = 0f;

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        HandleMessageCooldown();
    }

    private void HandleMessageCooldown()
    {
        if (!MessageOnCooldown()) return;

        currentMessageCooldown -= Time.deltaTime;
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("There is more than one MessageManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void CreateMessage(string message, Color messageColor, Vector3 position, float duration)
    {
        if (MessageOnCooldown()) return;

        Transform messagePrefabTransform = Instantiate(messagePrefab);

        MessageHandler messageHandler = messagePrefabTransform.GetComponentInChildren<MessageHandler>();

        if (messageHandler == null) return;

        messageHandler.SetMessage(_camera, message, messageColor, position, duration);   
        SetMessageOnCooldown();
    }

    public void CreateMessage(string message, Vector3 position, float duration) => CreateMessage(message, defaultMessageColor, position, duration);
    public void CreateMessage(string message, Color messageColor, float duration) => CreateMessage(message, messageColor, defaultMessageTransform.position, duration);
    public void CreateMessage(string message, Color messageColor, Vector3 position) => CreateMessage(message, messageColor, position, defaultMessageDuration);

    public void CreateMessage(string message, Color messageColor) => CreateMessage(message, messageColor, defaultMessageTransform.position, defaultMessageDuration);
    public void CreateMessage(string message, Vector3 position) => CreateMessage(message, defaultMessageColor, position, defaultMessageDuration);
    public void CreateMessage(string message, float duration) => CreateMessage(message, defaultMessageColor, defaultMessageTransform.position, duration);

    public void CreateMessage(string message) => CreateMessage(message, defaultMessageColor, defaultMessageTransform.position, defaultMessageDuration);

    private bool MessageOnCooldown() => currentMessageCooldown > 0;
    private void SetMessageOnCooldown() => currentMessageCooldown = messageCooldown;
}
