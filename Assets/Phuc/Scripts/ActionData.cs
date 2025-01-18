using System;
using UnityEngine;

public class ActionData
{

}

[Serializable]
public class JumpActionData : ActionData //For HappyObstacle
{
    public Transform startPoint;
    public Transform endPoint;
    public float height;
    public float duration;
}

[Serializable]
public class AttackActionData : ActionData //For AngryObstacle
{
    public Transform pointToAttack;
}

[Serializable]
public class DuckActionData : ActionData //For DepressedObstacle
{
    public Transform pointToStartDuck;
    public Transform pointToEndDuck;
}

[Serializable]
public class FlyActionData : ActionData //For LoveObstacle
{
    public Transform startPoint;
    public Transform endPoint;
}