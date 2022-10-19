using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// (기본) 몬스터 IDLE 상태 
/// </summary>
public class BasicMonsterIdle : BaseState
{
    BasicMonster monster;

    // IDLE 상태 정의
    public BasicMonsterIdle(BasicMonster stateMachine) : base("IDLE", stateMachine)
    {
        monster = (BasicMonster)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        //if (Input.GetKeyDown(KeyCode.Escape))
        if (monster.distance < 0.0f) return;
        if (monster.distance <= monster.idleRange)
        {
            stateMachine.ChangeState(((BasicMonster)stateMachine).moveState);
        }
    }

    public override void UpdateLate()
    {
        base.UpdateLogic();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
