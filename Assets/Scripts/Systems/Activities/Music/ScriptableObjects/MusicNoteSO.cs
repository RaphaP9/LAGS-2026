using UnityEngine;

[CreateAssetMenu(fileName = "NewMusicNoteSO", menuName = "ScriptableObjects/Activities/Music/MusicNote")]
public class MusicNoteSO : ScriptableObject
{
    public int id;
    public string noteName;
    public AudioClip soundClip;
}
