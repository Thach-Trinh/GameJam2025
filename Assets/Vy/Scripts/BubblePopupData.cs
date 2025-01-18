using System;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;

namespace VyNS
{
    [Serializable]
    public class BubblePopupData
    {
        [SerializeField] private List<EmotionBubbleVisualData> emotionVisuals;
        [SerializeField] private float selectionTime = 3f;
        
        public void SetEmotionVisuals(List<EmotionBubbleVisualData> emotionVisuals)
        {
            this.emotionVisuals = emotionVisuals;
        }
        
        public List<EmotionBubbleVisualData> GetEmotionVisuals()
        {
            return null;
        }
        
        public float SelectionTime => selectionTime;
    }

    [Serializable]
    public class EmotionBubbleVisualData
    {
        public EmotionType emotionType;
        public string textDescription;
    }
}