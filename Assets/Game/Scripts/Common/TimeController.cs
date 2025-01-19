using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    public List<ITimeReactive> affectedObjects = new List<ITimeReactive>();
    public const float defaultTimeScale = 1f;
    public float curTimeScale;
    public float slowTimeScale;

    private void Awake()
    {
        Debug.Log("Ahihi");
        Instance = this;
    }
    
    
    [ContextMenu("Get All Affected Objects")]
    public void GetAllAffectedObjects()
    {
        affectedObjects.Clear();
        ITimeReactive[] objects = FindObjectsOfType<MonoBehaviour>().OfType<ITimeReactive>().ToArray();
        affectedObjects.AddRange(objects);
        Debug.Log(affectedObjects.Count);
    }

    public void SetTimeScale(float timeScale)
    {
        this.curTimeScale = timeScale;
        affectedObjects.Iterate(x => x.OnTimeScaleChanged(timeScale));
    }

    public void TriggerSlowMotion() => SetTimeScale(curTimeScale);
    public void TriggerNormalMotion() => SetTimeScale(defaultTimeScale);

}
