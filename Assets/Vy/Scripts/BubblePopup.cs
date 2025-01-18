using System;
using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VyNS;

public class BubblePopup : MonoBehaviour
{
    public event Action<EmotionType> OnBubblePopupSelectedEvent;
    
    [SerializeField] private BubblePopupData data;
    [SerializeField] private VyNS.ObjectPool objectPool;

    [SerializeField, Header("Horizontal Layout")] private RectTransform horizontalBackground;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private float horizontalSpacing = 30;
    [SerializeField] private float itemWidth = 115;
    [SerializeField] private float itemCountInMinWidthLayout = 3;
    [SerializeField] private float offsetHorizontalBackground = 30;
    [SerializeField] private List<BubbleItem> bubbleItems;

    [SerializeField, Header("Animation")] private Animator animator;
    [SerializeField] private string triggerShow = "Show";
    [SerializeField] private string triggerHide = "Hide";
    private bool isShowing = false;
    private bool isHiding = false;
    
    [SerializeField, Header("Debug")] private bool enableLog = true;
    [SerializeField] private string logTag = $"{nameof(BubblePopup)} ";

    [SerializeField] private Slider slider;
    private Coroutine timerCoroutine;

    public void Initialize(BubblePopupData data)
    {
        if (data == null)
        {
            VyHelper.PrintError(enableLog, logTag, "BubblePopupData is null.");
            return;
        }
        
        UpdateHorizontalLayout(data.EmotionBubbleVisualDatas);

        StartTimer(data.SelectionTime);
    }

    private void UpdateHorizontalLayout(List<EmotionBubbleVisualData> emotionVisuals)
    {
        foreach (var emotionVisual in emotionVisuals)
        {
            var bubbleItem = GetBubbleItem(emotionVisual);
            bubbleItems.Add(bubbleItem);
            if (bubbleItem.transform.parent != horizontalLayoutGroup.transform)
                bubbleItem.transform.SetParent(horizontalLayoutGroup.transform, false);
        }
        
        ResizeBubbleHorizontal();
    }
    
    private void StartTimer(float timer)
    {
        StartCoroutine(TimerRoutine(timer));
    }

    private IEnumerator TimerRoutine(float timer)
    {
        slider.maxValue = 1;
        slider.value = 0;
        float time = 0;
        while (time <= timer)
        {
            var t = time / timer;
            slider.value = t;
            yield break;
            time += Time.deltaTime;
        }

        slider.value = 1;
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private void OnDisable()
    {
        StopTimer();
    }

    public void Uninitialize()
    {
        foreach (var bubbleItem in bubbleItems)
        {
            bubbleItem.Uninitialize();
            objectPool.ReturnObject(bubbleItem.gameObject);
        }

        bubbleItems.Clear();
    }

    public void Show()
    {
        if (isShowing)
            return;
        isShowing = true;
        animator.SetTrigger(triggerShow);
    }

    public async void OnShowDone()
    {
        float delay = 0.2f;
        foreach (var bubbleItem in bubbleItems)
        {
            bubbleItem.OnShow();
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
        }

        isShowing = false;
    }

    public void OnUserSelected(EmotionType emotionType, int index)
    {
        VyHelper.PrintLog(enableLog, logTag, $"User selected {emotionType}, index {index}");
    }

    public void Hide()
    {
        if (isHiding)
            return;
        isHiding = true;
        animator.SetTrigger(triggerHide);
    }

    public void OnHideDone()
    {
        isHiding = false;
    }

    private BubbleItem GetBubbleItem(EmotionBubbleVisualData visualData)
    {
        var bubbleItem = objectPool.GetObject().GetComponent<BubbleItem>();
        bubbleItem.Initialize(visualData);
        return bubbleItem;
    }
    
    [ContextMenu("ResizeBubbleHorizontal")]
    public void ResizeBubbleHorizontal()
    {
        var itemCount = horizontalLayoutGroup.transform.childCount;
        var newWidth = itemCount * itemWidth + (itemCount - 1) * horizontalSpacing + offsetHorizontalBackground * 2;
        VyHelper.PrintLog(enableLog, logTag, $"newWidth {newWidth} HorizontalLayoutMinWidth {HorizontalLayoutMinWidth}");
        
        newWidth = Mathf.Max(newWidth, HorizontalLayoutMinWidth);
        var newSize = new Vector2(newWidth, horizontalBackground.sizeDelta.y);
        horizontalBackground.sizeDelta = newSize;
    }

    private float HorizontalLayoutMinWidth => itemCountInMinWidthLayout * itemWidth + offsetHorizontalBackground * 2;
}