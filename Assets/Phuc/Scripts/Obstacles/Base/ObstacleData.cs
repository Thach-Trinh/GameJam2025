using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ObstacleData")]
public class ObstacleData : ScriptableObject
 {
     public List<EmotionData> _correctEmotions;
     public List<EmotionData> _incorrectEmotions;
     public int timeToAnswer;
     public string obstacleDialogue;
     public bool shouldSlowMotion;
 }

