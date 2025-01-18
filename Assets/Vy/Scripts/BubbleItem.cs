using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VyNS
{
    public class BubbleItem : MonoBehaviour
    {
        [SerializeField] private object BubbleData;
        [SerializeField] private GameObject[] listEmotionImages;
        [SerializeField] private TextMeshProUGUI textDescription;
        [SerializeField, Header("Animation")] private Animator animator;
        [SerializeField] private string triggerShow = "Show";
        [SerializeField] private string triggerSelect = "Select";
        
        [SerializeField, Header("Debug")] private bool enableLog = true;
        [SerializeField] private string logTag = $"{nameof(BubbleItem)} ";
        

        public void Initialize(EmotionBubbleVisualData visualData)
        {
            UpdateEmotion(visualData.emotionType);
            UpdateDescription(visualData.textDescription);
            animator.SetTrigger(triggerShow);
        }

        public void Uninitialize()
        {
            
        }

        public void OnSelected()
        {
            animator.SetTrigger(triggerSelect);
        }

        public void OnShow()
        {
            animator.SetTrigger(triggerShow);
        }

        private void UpdateEmotion(EmotionType emotionType)
        {
            TurnOffAllEmotionImages();
            GetEmotionObject(emotionType).SetActive(true);
        }
        
        private void UpdateDescription(string description)
        {
            textDescription.text = description;
        }
        
        private void TurnOffAllEmotionImages()
        {
            foreach (var emotionImage in listEmotionImages)
            {
                emotionImage.SetActive(false);
            }
        }
        
        private GameObject GetEmotionObject(EmotionType emotionType)
        {
            var index = (int)emotionType;
            if (index < 0 || index >= listEmotionImages.Length)
            {
                VyHelper.PrintWarning(enableLog, logTag, $"EmotionType {emotionType} is out of range.");
                return null;
            }
            
            return listEmotionImages[index];
        }
    }
}