using UnityEngine;

public static class VyHelper
{
    public static void PrintLog(bool enableLog, string tag, string message)
    {
        if (enableLog)
        {
            Debug.Log(tag + message);
        }
    }
    
    public static void PrintWarning(bool enableLog, string tag, string message)
    {
        if (enableLog)
        {
            Debug.LogWarning(tag + message);
        }
    }
    
    public static void PrintError(bool enableLog, string tag, string message)
    {
        if (enableLog)
        {
            Debug.LogError(tag + message);
        }
    }
}