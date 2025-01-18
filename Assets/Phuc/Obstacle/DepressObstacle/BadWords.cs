using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWords : DepressedObstacleView
{
    [SerializeField] private Transform badWordsTransform;
    [SerializeField] private float speed;
    [SerializeField] private float destroyDuration;
    protected override IEnumerator DepressedObstacleExitTriggerBoxAnimation()
    {
        float ticker = 0;
        while (ticker < destroyDuration)
        {
            ticker += Time.deltaTime;
            badWordsTransform.position += Vector3.left * (speed * Time.deltaTime);
            yield return null;
        }
        Destroy(badWordsTransform.gameObject);
    }
}
