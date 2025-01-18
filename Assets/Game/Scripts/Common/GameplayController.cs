using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;
    public Transform endPoint;
    public float height;
    public float duration;
    public Transform startPoint;
    public TimeController timeController => TimeController.Instance;
    private Player player => Player.Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player.ChangeState(ActionType.Idle);
    }
    private void Update()
    {
        player.CustomUpdate(timeController.curTimeScale);
        Cheat();
    }

    private void Cheat()
    {
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.ChangeState(ActionType.Attack);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.ChangeState(ActionType.Duck);
        }
    }

    private void LateUpdate()
    {
        CameraController.Instance.Follow(player.transform.position);
    }
}
