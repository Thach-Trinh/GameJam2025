using System.Collections;
using System.Collections.Generic;
using BubblePopupNS;
using UnityEngine;
using VyNS;

public class BubblePopupController : MonoBehaviour
{
    private static BubblePopupController instance;
    public static BubblePopupController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BubblePopupController>();
            }
            return instance;
        }
    }
    
    [SerializeField, Header("Popup")] private GameObject bubblePopupPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private BubblePopup bubblePopup;
    [SerializeField] private bool initPopupOnStart = true;

    [SerializeField, Header("Debug")] private bool enableLog = true;
    [SerializeField] private string logTag = $"{nameof(BubblePopupController)} " ;
    [SerializeField] private VyTestSO testSO; //TODO: delete later

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            VyHelper.PrintWarning(enableLog, logTag,"BubblePopupController already exists. Deleting new instance.");
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowPopup();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            HidePopup();
        }
    }

    private BubblePopup CreatePopup()
    {
        var popup = Instantiate(bubblePopupPrefab, canvas.transform).GetComponent<BubblePopup>();
        if (popup == null)
        {
            VyHelper.PrintError(enableLog, logTag, "BubblePopup prefab does not have BubblePopup component.");
            return null;
        }

        return popup;
    }

    private void ShowPopup()
    {
        if (bubblePopup == null)
            bubblePopup = CreatePopup();
        
        //NOTE: Fake scenario
        var data = testSO.GetBubblePopupData();
        //---

        bubblePopup.OnBubblePopupSelectedEvent -= OnBubblePopupSelectedEventHandler;
        bubblePopup.OnBubblePopupSelectedEvent += OnBubblePopupSelectedEventHandler;
        bubblePopup.Initialize(data);
    }

    private void OnBubblePopupSelectedEventHandler(EmotionType emotionType)
    {
        VyHelper.PrintLog(enableLog, logTag, $"User selected {emotionType}");
        HidePopup();
    }

    private void HidePopup()
    {
        if (bubblePopup == null)
        {
            VyHelper.PrintWarning(enableLog, logTag, "BubblePopup is null.");
            return;
        }
        
        bubblePopup.Uninitialize();
        bubblePopup.Hide();
    }
}
