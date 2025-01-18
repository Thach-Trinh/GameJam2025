using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EmotionData", menuName = "EmotionData")]
public class EmotionData : ScriptableObject
{
    public Emotion emotion;
    public Sprite emoIcon;
    public string emoDescription;
}
public enum Emotion
{
    HAPPY, //Vui
    DEPRESSED, //Buồn
    ANGRY, //Tức giận
    LOVE, //Yêu
    CONFIDENT, //Tự tin
    SELF_DEPRECATING, //Tự ti
    SCARED, //Sợ
}
