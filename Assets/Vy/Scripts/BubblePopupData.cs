using System;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;

namespace VyNS
{
    [CreateAssetMenu(fileName = "new BubblePopupData", menuName = "Vy/BubblePopupData")]
    public class BubblePopupData : ScriptableObject
    {
        [SerializeField] private List<EmotionBubbleVisualData> emotionVisuals;
        [SerializeField] private float selectionTime = 3f;
        [SerializeField] private string hint;
        
        public string Hint => hint;
        public List<EmotionBubbleVisualData> EmotionBubbleVisualDatas => emotionVisuals;
        public float SelectionTime => selectionTime;
    }

    [Serializable]
    public class EmotionBubbleVisualData
    {
        public EmotionType emotionType;
        public string textDescription;
    }
}