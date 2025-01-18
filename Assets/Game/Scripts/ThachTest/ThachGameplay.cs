using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThachGameplay : MonoBehaviour
{
    public Player player;
    public float timeScale;
    public Transform endPoint;
    public float height;
    public float duration;
    public Transform startPoint;


    private void Update()
    {
        player.CustomUpdate(timeScale);
        CameraController.Instance.Follow(player.transform.position);
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeState(ActionType.Run);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            JumpActionData jumpData = new JumpActionData()
            {
                startPoint = startPoint,
                endPoint = endPoint,
                height = height,
                duration = duration
            };
            player.ChangeState(ActionType.Jump, jumpData);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            player.ChangeState(ActionType.Idle);
            player.transform.position = startPoint.position;
        }
    }


}
