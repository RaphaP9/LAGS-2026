using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSFXPoolSO", menuName = "ScriptableObjects/Audio/SFXPool")]
public class SFXPool : ScriptableObject
{
    [Header("General")]
    public AudioClip[] sfxA;

    [Header("Player")]
    public AudioClip[] playerHarvesting;
    public AudioClip playerFootsteps;

    [Header("Fishing")]
    public AudioClip pullingFish;
    public AudioClip[] startFishing;
    public AudioClip[] throwFishingRod;
    public AudioClip[] fishingFail;
    public AudioClip[] fishingSuccess;

    [Header("Weaving")]
    public AudioClip[] sew;

    [Header("General")]
    public AudioClip[] success;
    public AudioClip[] fail;
}
