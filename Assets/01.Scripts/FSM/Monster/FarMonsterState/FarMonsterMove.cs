//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
///// <summary>
///// 원거리 몬스터 이동 스크립트
///// </summary>
//public class FarMonsterMove : BaseState
//{
//    BasicFarMonster monster;
//    Transform target;

//    // Move 생성자
//    public FarMonsterMove(BasicFarMonster stateMachine) : base("MOVE", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }


//    #region MOVE

//    private void Move()
//    {
//        if (target == null) return;
//        monster.LookTarget(target);
//        monster.agent.SetDestination(target.position);
//    }
//    private void SetMove(bool isMove)
//    {
//        monster.agent.isStopped = !isMove;
//    }

//    #endregion

//    #region ANIMATION

//    public override void SetAnim(bool isPlay)
//    {
//        base.SetAnim();
//        monster.MoveAnimation(isPlay);
//    }

//    #endregion

//    #region STATE

//    // 다른 STATE로 넘어가는 조건
//    public override void CheckDistance()
//    {
//        base.CheckDistance();
//        if (monster.distance <= monster.attackRange)
//        {
//            stateMachine.ChangeState(monster.idleState);
//        }
//    }

//    // NavMesh를 이용하여 이동
//    // 멈춰있다면 isStopped=false
//    // 애니메이션 실행
//    // 타겟 쫓아가기 시작
//    public override void Enter()
//    {
//        base.Enter();

//        SetMove(true);
//        SetAnim(true);
//    }

//    // 타겟 계속 찾으며 쫓아가기 + 쳐다보기
//    // 남은 거리에 따라 상태 전환
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        target = monster.SerachTarget();

//        Move();
//    }

//    // 상태 끝났을 시
//    // 애니메이션 끄고 움직임 멈추기
//    public override void Exit()
//    {
//        base.Exit();
//        SetAnim(false);
//        SetMove(false);
//    }
//    #endregion
//}
