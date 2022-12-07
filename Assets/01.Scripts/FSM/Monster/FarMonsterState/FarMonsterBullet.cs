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
    Rigidbody rigid;

    float moveSpeed = 20f;
    float deleteTime = 2.0f;

    int damage = 10;

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