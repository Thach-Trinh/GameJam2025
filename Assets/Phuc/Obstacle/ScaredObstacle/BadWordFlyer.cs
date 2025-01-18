using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BadWordFlyer : ScaredObstacleView
{
    [SerializeField] private List<Transform> _flyItems;
    [SerializeField] private float delayEachItem;
    [SerializeField] private Transform stopPosition;
    [SerializeField] private Transform upwardPosition;
    [SerializeField] private float speed;
    [SerializeField] private float destroyDuration;
    protected override IEnumerator PlayScaredAnimationChoiceCorrectIE()
    {
        for (int i = 0; i < _flyItems.Count; i++)
        {
            StartCoroutine(FlyItem_Stop_Up(_flyItems[i]));
            yield return new WaitForSeconds(delayEachItem);
        }
    }

    protected override IEnumerator PlayScaredAnimationChoiceInCorrectIE()
    {
        for (int i = 0; i < _flyItems.Count; i++)
        {
            StartCoroutine(FlyItemBackward(_flyItems[i]));
            yield return new WaitForSeconds(delayEachItem);
        }
    }
    
    IEnumerator FlyItem_Stop_Up(Transform item)
    {
        while (Vector2.Distance(item.position, stopPosition.position) > 0.01f)
        {
            item.position = Vector3.MoveTowards(item.position, stopPosition.position, speed * Time.deltaTime * _globalTimeScale);
            yield return null;
        }
        while (Vector2.Distance(item.position, upwardPosition.position) > 0.01f)
        {
            item.position = Vector3.MoveTowards(item.position, upwardPosition.position, speed * Time.deltaTime * _globalTimeScale);
            yield return null;
        }
        Destroy(item.gameObject);
    }
    
    IEnumerator FlyItemBackward(Transform item)
    {
        float ticker = 0;
        while (ticker < destroyDuration)
        {
            ticker += Time.deltaTime;
            item.position += Vector3.left * (speed * Time.deltaTime* _globalTimeScale);
            yield return null;
        }
        Destroy(item.gameObject);
    }
}
