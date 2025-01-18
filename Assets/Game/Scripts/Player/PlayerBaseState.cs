using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class PlayerBaseState : MonoBehaviour
{
    protected Player player;
    [SerializeField] protected ActionType type;
    public ActionType Type => type;

    public virtual void Init(Player player) => this.player = player;

    public abstract void EnterState(object[] data);
    public abstract void UpdateState(float deltaTime, float timeScale);
    public abstract void ExitState();
}
