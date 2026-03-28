using UnityEngine;

public class CustomSFXManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected SFXPool SFXPool;

    [Header("Debug")]
    [SerializeField] protected bool debug;

    protected AudioSource audioSource;

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void StopAudioSource()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    protected void PauseAudioSource()
    {
        if (audioSource.clip == null) return;
        audioSource.Pause();
    }

    protected void ResumeAudioSource()
    {
        if (audioSource.clip == null) return;
        audioSource.Play();
    }

    protected void ReplaceAudioClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = null;

        if (!clip)
        {
            if (debug) Debug.Log($"The clip {clip.name} is null");
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}
