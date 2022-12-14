using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 원거리 몬스터의 발사체 스크립트
/// 이 스크립트에서는 공격 성공 유무와 이동만 다룸
/// </summary>
public class FarMonsterBullet : Poolable
{
    MonsterData monsterDB;
    Rigidbody rigid;

    float moveSpeed = 20f;
    float deleteTime = 2.0f;

    private int damage = 20;
    public void SetDamage(int setDamage) { damage = setDamage; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Invoke("DeleteBullet", deleteTime);
    }

    // 발사체 이동
    void FixedUpdate()
    {
        if (rigid != null && moveSpeed > 0)
        {
            rigid.position += transform.forward * (moveSpeed * Time.deltaTime);
        }
    }

    public override void ResetData()
    {
        // 초기화
        // 아직 없음
    }

    // 플레이어 공격 성공 시
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHp>().Hit(damage);
            //팝업
            PopupData popupData = PopupData.Original;
            popupData.defaultColor = Color.red;
            GameManager.Instance.UI.SpawnDamagePopup(other.transform, damage, popupData);
            DeleteBullet();
        }
    }

    // 해당 시간 이후 자동 삭제
    private void DeleteBullet()
    {
        EffectDelete();
        GameManager.Instance.Pool.Push(this);
    }

    // 사라질 때 이펙트
    private void EffectDelete()
    {
        // 사운드
        // 파티클 등등
    }
}
