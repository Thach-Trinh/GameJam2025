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
    Vector2 startPoint;
    private void Awake()
    {
        startPoint = player.transform.position;
    }

    private void Update()
    {
        player.CustomUpdate(timeScale);
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeState(ActionType.Run);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            object[] jumpData = new object[] { (Vector2)(endPoint.position), height, duration };
            player.ChangeState(ActionType.Jump, jumpData);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            player.ChangeState(ActionType.Idle);
            player.transform.position = startPoint;
        }
    }


}
