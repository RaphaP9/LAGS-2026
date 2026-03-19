using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private MusicPoolSO musicPoolSO;

    [Header("Debug")]
    [SerializeField] private AudioClip currentMusic;

    private AudioSource audioSource;

    private void OnEnable()
    {
        ScenesManager.OnSceneLoadComplete += ScenesManager_OnSceneLoad;
    }

    private void OnDisable()
    {
        ScenesManager.OnSceneLoadComplete -= ScenesManager_OnSceneLoad;
    }

    private void Awake()
    {
        SetSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogWarning("There is more than one MusicManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip music)
    {
        if (music == currentMusic) return;

        if (!music)
        {
            StopMusic();
        }

        if (audioSource.clip != music)
        {
            audioSource.Stop();
            audioSource.clip = music;
            audioSource.Play();
        }

        currentMusic = audioSource.clip;
    }

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource.clip = null;
        currentMusic = null;
    }

    private void HandleScenesMusicPlay(string sceneName)
    {
        foreach(SceneNameMusic sceneNameMusic in musicPoolSO.sceneNameMusicList)
        {
            if(sceneNameMusic.sceneName == sceneName)
            {
                PlayMusic(sceneNameMusic.music);
                Debug.Log($"Music Play: {sceneNameMusic.music.name}");
                return;
            }
        }

        StopMusic();
        Debug.Log("No Music On Scene");
    }

    private void ScenesManager_OnSceneLoad(object sender, ScenesManager.OnSceneLoadEventArgs e)
    {
        HandleScenesMusicPlay(e.targetScene);
    }
}
