using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField][Header("Image")] private GameObject[] image;
    
    
    [Header("Movement Settings")]
    public RectTransform bubble;
    public float randomSwayAmountMinRange = 10;
    public float randomSwayAmountMaxRange = 30;
    
    public float swaySpeed = 1f;
    
    public float offsetEnd = 100;
    public float offsetStart = 100;
    public float durationMinRange = 1f;
    public float durationMaxRange = 5f;
    public float scaleMinRange = 0.5f;
    public float scaleMaxRange = 1.5f;
    public bool randomizeStart = true;
    
    private Vector2 startPosition;
    private Vector2 endPosition;
    
    public VyNS.ObjectPool objectPool;

    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        foreach (var item in image)
        {
            item.SetActive(false);
        }
        image[Random.Range(0, image.Length)].SetActive(true);
        
        
        DOTween.Kill(bubble);

        if (randomizeStart)
            startPosition = new Vector2(Random.Range(-Screen.width / 2, Screen.width / 2), -Screen.height / 2 - offsetStart);
        else
            startPosition = bubble.anchoredPosition;
        
        bubble.anchoredPosition = startPosition;

        endPosition = new Vector2(startPosition.x, Screen.height / 2 + offsetEnd);

        bubble.localScale = Vector3.one * Random.Range(scaleMinRange, scaleMaxRange);
        
        StartBubbleAnimation();
    }

    private void StartBubbleAnimation()
    {
        var duration = Random.Range(durationMinRange, durationMaxRange);
        bubble.DOAnchorPosY(endPosition.y, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                ReturnToPool();
            });

        var swayAmount = Random.Range(randomSwayAmountMinRange, randomSwayAmountMaxRange);
        bubble.DOAnchorPosX(startPosition.x + Random.Range(-swayAmount, swayAmount), swaySpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
            objectPool.ReturnObject(gameObject);
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDisable()
    {
        DOTween.Kill(bubble);
    }
}