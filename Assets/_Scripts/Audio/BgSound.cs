using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgSound : MonoBehaviour
{
    public static BgSound Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySoundHome();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Kiểm tra tên scene hoặc index của scene
        if (scene.name == "MainScene")
        {
            PlaySoundHome();
        }
        if (scene.name == "CreateMap")
        {
            audioSource.Stop();
        }
    }
    public void PlaySoundHome()
    {
        audioSource.clip = audioClips[0];
        SetVolume(0.4f);
        audioSource.Play();
    }

    public void PlaySoundGamePlay()
    {
        audioSource.clip = audioClips[1];
        SetVolume(0.4f);
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        // Đảm bảo volume nằm trong khoảng từ 0.0 đến 1.0
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        audioSource.volume = volume;
    }
}
