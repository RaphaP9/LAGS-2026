using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettingsSO", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Range(5, 20)] public int maxEnergy;
}
