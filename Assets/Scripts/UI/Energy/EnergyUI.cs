using TMPro;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI energyText;

    private void OnEnable()
    {
        EnergyManager.OnEnergyInitialized += EnergyManager_OnEnergyInitialized;
        EnergyManager.OnEnergyGained += EnergyManager_OnEnergyGained;
        EnergyManager.OnEnergySpent += EnergyManager_OnEnergySpent;
        EnergyManager.OnEnergyRefill += EnergyManager_OnEnergyRefill;
    }
    private void OnDisable()
    {
        EnergyManager.OnEnergyInitialized -= EnergyManager_OnEnergyInitialized;
        EnergyManager.OnEnergyGained -= EnergyManager_OnEnergyGained;
        EnergyManager.OnEnergySpent -= EnergyManager_OnEnergySpent;
        EnergyManager.OnEnergyRefill -= EnergyManager_OnEnergyRefill;
    }

    private void UpdateEnergyText(int energy) => energyText.text = energy.ToString();

    #region Subscriptions
    private void EnergyManager_OnEnergyInitialized(object sender, EnergyManager.OnEnergyEventArgs e)
    {
        UpdateEnergyText(e.energy);
    }
    private void EnergyManager_OnEnergyGained(object sender, EnergyManager.OnEnergyGainedEventArgs e)
    {
        UpdateEnergyText(e.newEnergy);
    }
    private void EnergyManager_OnEnergySpent(object sender, EnergyManager.OnEnergySpentEventArgs e)
    {
        UpdateEnergyText(e.newEnergy);
    }

    private void EnergyManager_OnEnergyRefill(object sender, EnergyManager.OnEnergyRefillEventArgs e)
    {
        UpdateEnergyText(e.newEnergy);
    }
    #endregion
}
