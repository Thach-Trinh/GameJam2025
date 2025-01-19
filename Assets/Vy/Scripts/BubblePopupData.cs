using System;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;

namespace VyNS
{
    [CreateAssetMenu(fileName = "new BubblePopupData", menuName = "Vy/BubblePopupData")]
    public class BubblePopupData : ScriptableObject
    {
        [SerializeField] public List<EmotionBubbleVisualData> emotionVisuals;
        [SerializeField] public float selectionTime = 3f;
        [SerializeField] public string hint;
        
        public string Hint => hint;
        public List<EmotionBubbleVisualData> EmotionBubbleVisualDatas => emotionVisuals;
        public float SelectionTime => selectionTime;
    }

    [Serializable]
    public class EmotionBubbleVisualData
    {
        public EmotionType emotionType;
        public string textDescription;
        public bool _isCorrect;
    }
}