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
        [SerializeField] private EmotionType emotionType;
        [SerializeField] private object BubbleData;
        [SerializeField] private BubbleIcon[] listEmotionImages;
        //[SerializeField] private TextMeshProUGUI textDescription;
        [SerializeField, Header("Debug")] private bool enableLog = true;
        [SerializeField] private string logTag = $"{nameof(BubbleItem)} ";
        
        public void Initialize(EmotionBubbleVisualData visualData)
        {
            emotionType = visualData.emotionType;
            TurnOffAllEmotions();
            GetEmotionObject(emotionType).gameObject.SetActive(true);
            UpdateDescription(visualData.textDescription);
        }

        public void Uninitialize()
        {
            TurnOffAllEmotions();
        }

        public void OnSelected()
        {
            GetEmotionObject(emotionType).Pop();
        }

        public void OnShow()
        {
            GetEmotionObject(emotionType).TurnOn();
        }

        private void UpdateDescription(string description)
        {
            //textDescription.text = description;
        }
        
        private void TurnOffAllEmotions()
        {
            foreach (var emotionImage in listEmotionImages)
            {
                emotionImage.TurnOff();
            }
        }
        
        private BubbleIcon GetEmotionObject(EmotionType emotionType)
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