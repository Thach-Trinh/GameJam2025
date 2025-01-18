using System.Collections.Generic;
using UnityEngine;

namespace VyNS
{
    [CreateAssetMenu(fileName = "VyTestSO", menuName = "Vy/VyTestSO")]
    public class VyTestSO : ScriptableObject
    {
        public BubblePopupData BubblePopupDataFake;
        public BubblePopupData GetBubblePopupData()
        {
            return BubblePopupDataFake;
        }
    }
}