using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGameState : MonoBehaviour
{
    public virtual void OnEnter(){}
    public virtual void OnExit(){}
    public virtual void OnUpdate(){}
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UserData userData;
    
    [SerializeField] private AbstractGameState menuState;
    [SerializeField] private AbstractGameState loadingState;
    [SerializeField] private AbstractGameState introState;
    [SerializeField] private AbstractGameState gameState;
    [SerializeField] private AbstractGameState restartState;
    [SerializeField] private AbstractGameState outroState;
    
    public AbstractGameState MenuState => menuState;
    public AbstractGameState LoadingState => loadingState;
    public AbstractGameState IntroState => introState;
    public AbstractGameState GameState => gameState;
    public AbstractGameState RestartState => restartState;
    public AbstractGameState OutroState => outroState;
    
    public UserData UserData => userData;
    
    private AbstractGameState currentState;

    private void Start()
    {
        SetState(MenuState);
    }

    public void SetState(AbstractGameState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        
        currentState = newState;
        currentState.OnEnter();
    }
}
