using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ActionType curAction;
    public ActionType nextAction;
    object[] data;
    public void ReceiveAction(ActionType type, params object[] data)
    {
        nextAction = type;
    }



    public void OnFinishObstacle()
    {
        nextAction = ActionType.Idle;
        data = null;
    }


}
