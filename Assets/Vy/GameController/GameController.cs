using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGameState : MonoBehaviour
{
    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void OnUpdate()
    {
    }
}

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private UserData userData;

    [SerializeField] private AbstractGameState menuState;
    [SerializeField] private AbstractGameState loadingState;
    [SerializeField] private AbstractGameState introState;
    [SerializeField] private AbstractGameState gameState;
    [SerializeField] private AbstractGameState restartState;
    [SerializeField] private AbstractGameState outroState;

    public bool IsMenuState => currentState == menuState;
    public AbstractGameState CurrentState => currentState;
    
    public AbstractGameState MenuState => menuState;
    public AbstractGameState LoadingState => loadingState;
    public AbstractGameState IntroState => introState;
    public AbstractGameState GameState => gameState;
    public AbstractGameState RestartState => restartState;
    public AbstractGameState OutroState => outroState;

    public UserData UserData => userData;

    private AbstractGameState currentState;
    public Player Player;
    public Transform PlayerSpawnPoint;
    public GameObject mapPrefab;
    private GameObject map;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetState(MenuState);
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void SetState(AbstractGameState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();
    }

    private void LateUpdate()
    {
        CameraController.Instance.Follow(Player.Instance.transform.position);
    }

    public void CreateMap()
    {
        map = Instantiate(mapPrefab);
    }

    public void DestroyMap()
    {
        Destroy(map);
        map = null;
    }
}