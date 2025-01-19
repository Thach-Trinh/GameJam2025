using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;

    public float height;
    public float duration;
    public Transform startPoint;
    public Transform endJumpPoint;
    public Transform endDuckPoint;
    public Transform endFlyPoint;
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
#if UNITY_EDITOR
        Cheat();
#endif
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
                endPoint = endJumpPoint,
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
            DuckActionData duckActionData = new DuckActionData()
            {
                pointToStartDuck = startPoint,
                pointToEndDuck = endDuckPoint
            };
            player.ChangeState(ActionType.Duck, duckActionData);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlyActionData flyActionData = new FlyActionData()
            {
                startPoint = startPoint,
                endPoint = endFlyPoint
            };
            player.ChangeState(ActionType.Fly, flyActionData);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.ChangeState(ActionType.Scream);
        }
    }

    private void LateUpdate()
    {
        CameraController.Instance.Follow(player.transform.position);
    }
}
