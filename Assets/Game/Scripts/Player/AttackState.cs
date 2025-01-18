using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerBaseState
{
    public float angrySpeed = 1f;
    public float[] skillSpeed;
    public float normalizedTransitionDuration = 0.1f;
    private static int ANGRY_HASH = Animator.StringToHash("GetAngry");
    private static int[] SKILL_HASHES;
    private static int CUR_ATK_INDEX = 0;
    //public int CUR_ATK_INDEX = 0;
    private const int SKILL_AMOUNT = 3;
    static AttackState()
    {
        SKILL_HASHES = new int[SKILL_AMOUNT];
        for (int i = 0; i < SKILL_AMOUNT; i++)
            SKILL_HASHES[i] = Animator.StringToHash($"Attack{i + 1}");
    }

    public override void EnterState(ActionData data)
    {
        player.SetStateAnimSpeed(angrySpeed);
        player.anim.CrossFade(ANGRY_HASH, normalizedTransitionDuration);
        player.onStartAttackEventTrigger = StartAttack;
        player.onFinishAttackEventTrigger = FinishAttack;

    }

    public void StartAttack()
    {
        player.anim.CrossFade(SKILL_HASHES[CUR_ATK_INDEX], normalizedTransitionDuration);
        player.SetStateAnimSpeed(skillSpeed[CUR_ATK_INDEX]);
        CUR_ATK_INDEX++;
        if (CUR_ATK_INDEX >= SKILL_AMOUNT)
            CUR_ATK_INDEX = 0;
    }

    public void FinishAttack()
    {
        player.ChangeState(ActionType.Run);
    }

    public override void UpdateState(float deltaTime, float timeScale)
    {
    }



    public override void ExitState()
    {
    }
}
