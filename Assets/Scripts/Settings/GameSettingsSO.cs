using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettingsSO", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettingsSO : ScriptableObject
{
    public Vector2 startingPlayerPosition;
    [Range(5, 20)] public int maxEnergy;
}
