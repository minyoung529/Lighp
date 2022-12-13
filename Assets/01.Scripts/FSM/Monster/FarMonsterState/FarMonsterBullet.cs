using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Ÿ� ������ �߻�ü ��ũ��Ʈ
/// �� ��ũ��Ʈ������ ���� ���� ������ �̵��� �ٷ�
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

    // �߻�ü �̵�
    void FixedUpdate()
    {
        if (rigid != null && moveSpeed > 0)
        {
            rigid.position += transform.forward * (moveSpeed * Time.deltaTime);
        }
    }

    public override void ResetData()
    {
        // �ʱ�ȭ
        // ���� ����
    }

    // �÷��̾� ���� ���� ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHp>().Hit(damage);
            //�˾�
            PopupData popupData = PopupData.Original;
            popupData.defaultColor = Color.red;
            GameManager.Instance.UI.SpawnDamagePopup(other.transform, damage, popupData);
            DeleteBullet();
        }
    }

    // �ش� �ð� ���� �ڵ� ����
    private void DeleteBullet()
    {
        EffectDelete();
        GameManager.Instance.Pool.Push(this);
    }

    // ����� �� ����Ʈ
    private void EffectDelete()
    {
        // ����
        // ��ƼŬ ���
    }
}