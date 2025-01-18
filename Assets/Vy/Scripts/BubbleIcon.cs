using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VyNS
{
    public class BubbleIcon : MonoBehaviour
    {
        [SerializeField] private GameObject bubbleShow;
        [SerializeField] private GameObject bubblePoped;

        public void TurnOn()
        {
            bubbleShow.SetActive(true);
            bubblePoped.SetActive(false);
        }

        public void TurnOff()
        {
            bubbleShow.SetActive(false);
            bubblePoped.SetActive(false);
        }

        public void Pop()
        {
            bubbleShow.SetActive(false);
            bubblePoped.SetActive(true);
        }
    }
}