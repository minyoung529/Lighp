//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// ���Ÿ� ���� ������ ��ũ��Ʈ
///// </summary>
//public class FarMonsterDamage : BaseState
//{
//    BasicFarMonster monster;

//    // ������
//    public FarMonsterDamage(BasicFarMonster stateMachine) : base("DAMAGED", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }

//    #region DAMAGE
//    float delayTime = 1.0f; // ���� �ð�
//    float nowDelay = 0.0f;
//    float damage = 20.0f;

//    private void SetDelay(float delay)
//    {
//        nowDelay = delay;
//    }

//    #endregion

//    #region ANIMATION

//    public override void SetAnim()
//    {
//        base.SetAnim();
//        monster.DamageAnimation();
//    }

//    #endregion

//    #region STATE

//    public override void CheckDistance()
//    {
//        base.CheckDistance();
//        if (monster.GetHP <= 0)
//        {
//            stateMachine.ChangeState(monster.dieState);
//        }
//        if (nowDelay >= delayTime)
//        {
//            stateMachine.ChangeState(monster.idleState);
//        }

//    }

//    // ���� ���� ��
//    // ���ط� ��ŭ ������ ���� + �ִϸ��̼� ����
//    // ���� HP�� 0 ���ϰ� �Ǹ� ���� Die�� ��ȯ
//    public override void Enter()
//    {
//        base.Enter();
//        SetDelay(0);
//        monster.SetHP(false, damage);
//        SetAnim();
//    }

//    // ���� �ð��� ������ ���� ��ȯ
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        nowDelay += Time.deltaTime;
//    }

//    // ���� ������ ��
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    #endregion
//}