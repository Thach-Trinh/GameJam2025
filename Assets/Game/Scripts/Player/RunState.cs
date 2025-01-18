using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    private Vector3 destination;
    protected float speed;
    protected Transform trans;

    public override void Init(Player player)
    {
        base.Init(player);
        trans = player.transform;
    }

    public override void EnterState(params object[] data)
    {
    }

    private void UpdateAnimSpeed()
    {
        //float animSpeed = 1;// controller.Character.Gear.HasGear(movementType) ? gearAnimSpeed : defaultAnimSpeed;
        ////controller.Character.Anim.speed = speed / animSpeed;
    }

    public override void UpdateState()
    {
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
