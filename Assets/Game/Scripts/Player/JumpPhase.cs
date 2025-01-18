using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JumpPhase : MonoBehaviour
{
    protected JumpState state;
    public void Init(JumpState state)
    {
        this.state = state;
    }
    public abstract void EnterPhase();
    public abstract void ExitPhase();
    public abstract void UpdatePhase();
}
