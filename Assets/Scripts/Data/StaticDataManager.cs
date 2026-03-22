using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    public static StaticDataManager Instance { get; private set; }

    [Header("Data")]
    [SerializeField] private Data data;

    public Data Data => data;

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogWarning("There is more than one StaticDataManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void ResetData() => data.ResetData();
}
