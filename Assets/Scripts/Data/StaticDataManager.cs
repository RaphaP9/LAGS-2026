using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    public static StaticDataManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Data")]
    [SerializeField] private Data data;

    public Data Data => data;

    private void Awake()
    {
        SetSingleton();
        ResetData(gameSettingsSO);
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

    public void ResetData(GameSettingsSO gameSettingsSO) => data.ResetData(gameSettingsSO);

    ///////////////////////////////////////////////////////////////////////////////////////////////
    
    public void SetCurrentEnergy(int energy) => data.currentEnergy = energy;
}
