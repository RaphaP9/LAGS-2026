using System;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Runtime Filled")]
    [SerializeField] private int currentEnergy;

    public int CurrentEnergy => currentEnergy;

    public static event EventHandler<OnEnergyEventArgs> OnEnergyInitialized;

    public static event EventHandler<OnEnergyGainedEventArgs> OnEnergyGained;
    public static event EventHandler<OnEnergySpentEventArgs> OnEnergySpent;
    public static event EventHandler<OnEnergyRefillEventArgs> OnEnergyRefill;

    public class OnEnergyEventArgs : EventArgs
    {
        public int energy;
    }

    public class OnEnergyGainedEventArgs: EventArgs
    {
        public int energyGained;
        public int newEnergy;
    }

    public class OnEnergySpentEventArgs : EventArgs
    {
        public int energySpent;
        public int newEnergy;
    }

    public class OnEnergyRefillEventArgs : EventArgs
    {
        public int energyRefilled;
        public int newEnergy;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeEnergy();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one FishingManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeEnergy()
    {
        currentEnergy = StaticDataManager.Instance.Data.currentEnergy;
        OnEnergyInitialized?.Invoke(this, new OnEnergyEventArgs { energy = currentEnergy });
    }

    public void GainEnergy(int quantity)
    {
        if (quantity <= 0) return;

        int newEnergy = currentEnergy + quantity > gameSettingsSO.maxEnergy? gameSettingsSO.maxEnergy : currentEnergy + quantity;
        int energyGained = newEnergy - currentEnergy;
        currentEnergy = newEnergy;

        OnEnergyGained?.Invoke(this, new OnEnergyGainedEventArgs { newEnergy = currentEnergy, energyGained = energyGained });

        SaveEnergyToData(currentEnergy);
    }

    public void SpendEnergy(int quantity)
    {
        if (quantity <= 0) return;

        int newEnergy = currentEnergy - quantity < 0 ? 0 : currentEnergy - quantity;
        int energySpent = currentEnergy - newEnergy;
        currentEnergy = newEnergy;

        OnEnergySpent?.Invoke(this, new OnEnergySpentEventArgs { newEnergy = currentEnergy, energySpent = energySpent });

        SaveEnergyToData(currentEnergy);
    }

    public void RefillEnergy(int quantity)
    {
        if (quantity <= 0) return;

        int newEnergy = gameSettingsSO.maxEnergy;
        int energyRefilled = newEnergy - currentEnergy;
        currentEnergy = newEnergy;

        OnEnergyRefill?.Invoke(this, new OnEnergyRefillEventArgs { newEnergy = currentEnergy, energyRefilled = energyRefilled });

        SaveEnergyToData(currentEnergy);
    }

    public bool CanSpendEnergy(int quantity)
    {
        if (quantity > currentEnergy) return false;
        return true;
    }

    private void SaveEnergyToData(int energy) => StaticDataManager.Instance.SetCurrentEnergy(energy);
}
