using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundSO", menuName ="SO/Sound")]
public class SoundSO : ScriptableObject
{
    public Sound[] arrSound;
}


[Serializable]
public class Sound
{
    public SoundName name; 
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [HideInInspector]
    public AudioSource source; 
    public bool loop = false;
}


public enum SoundName
{
    GAMEPLAY,
    OUTRO,
    POP_BUBBLE,
    ALARM,
    THUD,
    FAILED,
    JUMP,
    SCREAM,
    FLY,
    DUCK,
    WHISPERS,
    BOSS_YELLING,
    ITEM_FLYER,
    FOOTSTEP
}
