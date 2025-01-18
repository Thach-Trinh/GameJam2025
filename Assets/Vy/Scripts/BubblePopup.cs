using System;
using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
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
    [SerializeField] private bool isShowing = false;
    [SerializeField] private bool isHiding = false;
    
    [SerializeField, Header("User")] private bool userSelected = false;
    
    [SerializeField, Header("Slider")] private Slider slider;
    
    [SerializeField, Header("Dialog")] private GameObject dialog;
    [SerializeField] private TextMeshProUGUI dialogText;

    [SerializeField, Header("Debug")] private bool enableLog = true;
    [SerializeField] private string logTag = $"{nameof(BubblePopup)} ";
    [SerializeField] private float currentTime = 0;

    private Coroutine timerCoroutine;

    public void Initialize(BubblePopupData data)
    {
        this.data = data;
        Uninitialize();
        if (data == null)
        {
            VyHelper.PrintError(enableLog, logTag, "BubblePopupData is null.");
            return;
        }
        
        UpdateHorizontalLayout(data.EmotionBubbleVisualDatas);
        UpdateDialog(data.Hint);
        slider.value = 1;
    }
    
    private void UpdateDialog(string text)
    {
        dialogText.text = text;
        dialog.SetActive(!string.IsNullOrEmpty(text));
    }

    private void UpdateHorizontalLayout(List<EmotionBubbleVisualData> emotionVisuals)
    {
        int i = 0;
        foreach (var emotionVisual in emotionVisuals)
        {
            var bubbleItem = GetBubbleItem(emotionVisual, i);
            bubbleItems.Add(bubbleItem);
            if (bubbleItem.transform.parent != horizontalLayoutGroup.transform)
                bubbleItem.transform.SetParent(horizontalLayoutGroup.transform, false);
            i++;
        }
        
        ResizeBubbleHorizontal();
    }
    
    private void StartTimer(float timer)
    {
        timerCoroutine = StartCoroutine(TimerRoutine(timer));
    }

    private IEnumerator TimerRoutine(float timer)
    {
        slider.maxValue = 1;
        slider.value = 0;
        currentTime = 0;
        while (currentTime <= timer)
        {
            var t = Mathf.Clamp01(1 - currentTime / timer);
            slider.value = t;
            yield return null;
            currentTime += Time.deltaTime;
        }

        slider.value = 0;
        timerCoroutine = null;
        VyHelper.PrintLog(enableLog, logTag, "Timer done");
        Hide();
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
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

    [ContextMenu("Show")]
    public void Show()
    {
        if (isShowing)
            return;
        isShowing = true;
        userSelected = false;
        animator.SetTrigger(triggerShow);
    }

    public void OnShowDone()
    {
        VyHelper.PrintLog(enableLog, logTag, "OnShowDone");
        float delay = 0.2f;
        foreach (var bubbleItem in bubbleItems)
        {
            bubbleItem.OnShow();
        }

        isShowing = false;
        StartTimer(data.SelectionTime);
    }

    public async void OnUserSelected(EmotionType emotionType, int index, BubbleItem item)
    {
        if (userSelected || currentTime >= data.SelectionTime)
            return;
        userSelected = true;
        item.Pop();
        StopTimer();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        Hide();
        VyHelper.PrintLog(enableLog, logTag, $"User selected {emotionType}, index {index}");
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        if (isHiding)
            return;
        isHiding = true;
        StopTimer();
        animator.SetTrigger(triggerHide);
    }

    public void OnHideDone()
    {
        VyHelper.PrintLog(enableLog, logTag, "OnHideDone");
        isHiding = false;
        isShowing = false;
        //Uninitialize();
    }

    private BubbleItem GetBubbleItem(EmotionBubbleVisualData visualData, int index)
    {
        var bubbleItem = objectPool.GetObject().GetComponent<BubbleItem>();
        bubbleItem.Initialize(visualData, this, index);
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