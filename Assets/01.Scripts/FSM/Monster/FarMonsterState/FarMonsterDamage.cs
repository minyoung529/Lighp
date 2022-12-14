//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// 원거리 몬스터 데미지 스크립트
///// </summary>
//public class FarMonsterDamage : BaseState
//{
//    BasicFarMonster monster;

//    // 생성자
//    public FarMonsterDamage(BasicFarMonster stateMachine) : base("DAMAGED", stateMachine)
//    {
//        monster = (BasicFarMonster)stateMachine;
//    }

//    #region DAMAGE
//    float delayTime = 1.0f; // 무적 시간
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

//    // 상태 시작 시
//    // 피해량 만큼 데미지 감소 + 애니메이션 실행
//    // 만약 HP가 0 이하가 되면 상태 Die로 변환
//    public override void Enter()
//    {
//        base.Enter();
//        SetDelay(0);
//        monster.SetHP(false, damage);
//        SetAnim();
//    }

//    // 일정 시간이 지나면 상태 변환
//    public override void UpdateLogic()
//    {
//        base.UpdateLogic();
//        nowDelay += Time.deltaTime;
//    }

//    // 상태 끝났을 시
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    #endregion
//}
