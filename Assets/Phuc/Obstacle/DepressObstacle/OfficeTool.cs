using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeTool : DepressedObstacleView
{
    [SerializeField] private Transform leftTransform;
    [SerializeField] private Transform rightTransform;
    [SerializeField] private List<Transform> toolTransforms;
    [SerializeField] private float speed;
    protected override IEnumerator DepressedObstacleEnterTriggerBoxAnimation()
    {
        StartCoroutine(MoveToolFromTo(toolTransforms[0]));
        StartCoroutine(MoveToolFromTo(toolTransforms[1]));
        StartCoroutine(MoveToolFromTo(toolTransforms[2]));
        yield return null;
    }
    
    IEnumerator MoveToolFromTo(Transform itemTfm)
    {
        float itemTfmX = itemTfm.position.x;
        bool _isMoveRight = Mathf.Abs(itemTfmX - rightTransform.position.x) < Mathf.Abs(itemTfmX - leftTransform.position.x);
        float targetX = _isMoveRight ? rightTransform.position.x : leftTransform.position.x;
        while (true)
        {
            itemTfm.position = Vector3.MoveTowards(itemTfm.position, new Vector3(targetX, itemTfm.position.y, itemTfm.position.z), speed * Time.deltaTime);
            //flip item if reach target
            if (Mathf.Abs(itemTfm.position.x - targetX) < 0.01f)
            {
                itemTfm.localScale = new Vector3(-itemTfm.localScale.x, itemTfm.localScale.y, itemTfm.localScale.z);
                _isMoveRight = !_isMoveRight;
                targetX = _isMoveRight ? rightTransform.position.x : leftTransform.position.x;
            }
            yield return null;
        }
    }
}
