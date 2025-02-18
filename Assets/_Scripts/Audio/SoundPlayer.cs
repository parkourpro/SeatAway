using System;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public void PlaySoundBubbleSeat()
    {
        audioSource.clip = audioClips[0];
        SetVolume(1f);
        audioSource.Play();
    }

    public void PlaySoundSelect()
    {
        audioSource.clip = audioClips[1];
        SetVolume(1f);
        audioSource.Play();
    }
    public void PlaySoundDrop()
    {
        audioSource.clip = audioClips[2];
        SetVolume(1f);
        audioSource.Play();
    }
    public void PlaySoundBusCome()
    {
        audioSource.clip = audioClips[3];
        SetVolume(1f);
        audioSource.Play();
    }
    public void PlaySoundCusSit()
    {
        audioSource.clip = audioClips[4];
        SetVolume(1f);
        audioSource.Play();
    }
    public void PlaySoundWin()
    {
        audioSource.clip = audioClips[5];
        SetVolume(0.5f);
        audioSource.Play();
    }

    public void PlaySoundLoose()
    {
        audioSource.clip = audioClips[6];
        SetVolume(0.5f);
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        audioSource.volume = volume;
    }
}