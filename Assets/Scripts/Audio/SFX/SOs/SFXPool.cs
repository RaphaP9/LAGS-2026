using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSFXPoolSO", menuName = "ScriptableObjects/Audio/SFXPool")]
public class SFXPool : ScriptableObject
{
    [Header("General")]
    public AudioClip[] sfxA;
}
