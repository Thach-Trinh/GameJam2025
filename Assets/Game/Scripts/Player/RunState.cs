using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    public float speed;
    protected Transform trans;

    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }

    public override void EnterState(ActionData data)
    {
    }

    private void UpdateAnimSpeed()
    {
        //float animSpeed = 1;// controller.Character.Gear.HasGear(movementType) ? gearAnimSpeed : defaultAnimSpeed;
        ////controller.Character.Anim.speed = speed / animSpeed;
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
        trans.Translate(speed * deltaTime * timeScale * Vector2.right);
        ////speed = GetSpeed(controller.Character.Stat);
        //Vector3 oldPos = trans.position;
        //trans.position = Vector3.MoveTowards(trans.position, destination, speed * Time.deltaTime);
        //controller.Character.traveledDistance += Vector3.Distance(trans.position, oldPos);
        ////controller.Character.NamePanel.Follow(trans.position);
        //if (trans.position == destination)
        //{
        //    controller.Character.FinishTerrain();
        //}
    }


    public override void ExitState()
    {
    }
}
