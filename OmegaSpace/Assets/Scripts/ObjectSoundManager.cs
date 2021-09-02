using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class ObjectSoundManager : MonoBehaviour
{
    public AudioSource audioSource = null;
    private Queue<AudioClip> audioQueue = new Queue<AudioClip>(5);

    public bool IsOnPlay
    {
        get => audioSource.isPlaying;
    }
    public bool IsLoop
    {
        set => audioSource.loop = value;
        get => audioSource.loop;
    }

    public void ForcePlay(AudioClip audio)
    {
        if (IsOnPlay)
            audioSource.Stop();
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    private void OnEnable()
    {
        if(!audioSource)
           audioSource = GetComponent<AudioSource>();
    }
}
