using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;

public static class BubbleBridge
{
    public static ActionType GetAction(EmotionType emotion)
    {
        switch (emotion)
        {
            case EmotionType.Happy: return ActionType.Jump;
            case EmotionType.Love: return ActionType.Fly;
            case EmotionType.Angry: return ActionType.Attack;
            case EmotionType.Depressed: return ActionType.Duck;
            case EmotionType.Scared: return ActionType.Scream;
        }
        Debug.LogError("WTF");
        return default;
    }
}
