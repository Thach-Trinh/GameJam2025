using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private SoundSO soundSO;
    private Sound[] arrSound;

    private Sound bgMusic;
    private void Start()
    {
        CreateAudioSource();
        PlaySound(SoundName.GAMEPLAY);
    }

    private void CreateAudioSource()
    {
        var soundCount = soundSO.arrSound.Length;
        arrSound = new Sound[soundCount];
        for (int i = 0; i < soundCount; i++)
        {
            var sound = soundSO.arrSound[i];
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            arrSound[i] = sound;
        }
    }
    

    public void PlaySound(SoundName name)
    {
        Sound effect = Array.Find(arrSound, effect => effect.name == name);
        if (effect == null)
        {
            Debug.LogError("Unable to play effect " + name);
            return;
        }
        effect.source.Play();
    }
    
    public void StopSound(SoundName name)
    {
        Sound effect = Array.Find(arrSound, effect => effect.name == name);
        if (effect == null)
        {
            Debug.LogError("Unable to stop effect " + name);
            return;
        }
        effect.source.Stop();
    }
}

