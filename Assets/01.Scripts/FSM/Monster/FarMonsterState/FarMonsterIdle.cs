//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// (�⺻) ���Ÿ� ���� IDLE ���� 
///// </summary>
//public class FarMonsterIdle : BaseState
//{
//    BasicFarMonster monster;
//    Transform target = null;

//    // IDLE ���� ����
//    public FarMonsterIdle(BasicFarMonster stateMachine) : base("IDLE", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }

//    #region STATE

//    public override void CheckDistance()
//    {
//        base.CheckDistance();
//        if (target == null || !monster.LIVE) return;

//        if (monster.distance <= monster.attackRange)
//        {
//            stateMachine.ChangeState(monster.attackState);
//        }
//        else if (monster.distance <= monster.moveRange)
//        {
//            stateMachine.ChangeState(monster.moveState);
//        }
//    }

//    // ���� ���� ��
//    // Ÿ�� ã�� ����
//    public override void Enter()
//    {
//        base.Enter();
//    }

//    // Ÿ���� �ִٸ�
//    // �Ÿ��� ���� ���� ��ȯ
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        target = monster.SerachTarget();
//    }

//    // ���� ������ ��
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    #endregion
//}