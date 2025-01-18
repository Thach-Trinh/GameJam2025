using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public void OnAnimationDone()
    {
        GameController.Instance.SetState(GameController.Instance.GameState);
    }
}
