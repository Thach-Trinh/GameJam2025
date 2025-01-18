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

        [SerializeField] private BubblePopup popup;
        [SerializeField] private int index;
        
        public void Initialize(EmotionBubbleVisualData visualData, BubblePopup popup, int index)
        {
            this.popup = popup;
            this.index = index;
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
            popup.OnUserSelected(emotionType, index, this);
            
        }

        public void Pop()
        {
            GetEmotionObject(emotionType).Pop();
            AudioController.Instance.PlaySound(SoundName.POP_BUBBLE);
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