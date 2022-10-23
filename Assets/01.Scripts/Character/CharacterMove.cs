using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 캐릭터의 이동을 담당하는 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterMove : Character
{
    [SerializeField]
    protected MoveStat moveStat;
    public bool IsCurrentMoving
    {
        get
        {
            Vector2 temp = new Vector2(rigid.velocity.x, rigid.velocity.z);
            return (temp.sqrMagnitude > 0.01f);
        }
    }

    public Vector3 Velocity => rigid.velocity;
    protected bool isDash = false;
    protected bool isDoubleDash = false;

    [SerializeField]
    protected LayerMask blockLayer;

    #region CONTROL

    private bool canMove = true;
    private bool canJump = true;
    private float doubleDashTimer = Define.DASH_DOUBLE_TIME;
    private float dashCoolTimer = -1f;

    protected int jumpCount { get; private set; }
    #endregion

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        doubleDashTimer += Time.deltaTime;

        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
    }

    // 캐릭터를 velocity 방향으로 움직이는 함수
    protected void Move(Vector3 velocity, float speed = 1f, bool isRot = false, float rotTime = 0.5f)
    {
        if (!canMove) return;

        velocity *= speed;
        velocity.y = rigid.velocity.y;

        rigid.velocity = velocity;

        if (isRot && IsCurrentMoving)
        {
            ForwardToVelocity(rotTime);
        }

        OnMove(rigid.velocity);
    }

    // 보고 있는 방향을 가는 방향으로 설정해주는 함수
    protected void ForwardToVelocity(float rotSpeed)
    {
        Vector3 velocity = rigid.velocity;
        velocity.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotSpeed);
    }

    // 점프 함수
    protected void Jump(float jumpForce)
    {
        if (!canJump) return;
        if (jumpCount >= moveStat.maxJumpCount) return;

        jumpCount++;

        Vector3 vel = rigid.velocity;
        vel.y = 0f;

        rigid.velocity = vel;

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // velocity 방향으로 대시
    protected void Dash(Vector3 velocity)
    {
        // 쿨타임 검사
        if (dashCoolTimer > 0) return;

        // 더블 대쉬
        if (doubleDashTimer < Define.DASH_DOUBLE_TIME)
        {
            isDoubleDash = true;
            dashCoolTimer = Define.DASH_COOLTIME;
        }

        float distance = Define.DASH_DISTANCE;
        bool isUpdown = false;

        // 상하 보정
        if (velocity.x * velocity.z < 0)
        {
            distance *= 1.7f;
            isUpdown = true;
        }

        Vector3 destination = transform.position + velocity.normalized * distance;

        OnStartDash(isUpdown);
        rigid.DOKill();
        rigid.DOMove(destination, Define.DASH_DURATION).OnComplete(() => { OnEndDash(); });
    }

    // 움직일 때 자식 클래스에서 재정의할 함수
    protected virtual void OnMove(Vector3 velocity) { }

    /// <summary>
    /// 대쉬를 할 때 자식 클래스에서 재정의할 함수
    /// </summary>
    /// <param name="isUpDown">상하 보정이 필요한지 아닌지</param>
    protected virtual void OnStartDash(bool isUpDown)
    {
        // 초기화
        doubleDashTimer = 0f;
        isDash = true;
    }
    protected virtual void OnEndDash()
    {
        isDash = false;
        isDoubleDash = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        // 땅에 닿으면 jumpCount 초기화
        if (1 << layer == Define.BOTTOM_LAYER)
        {
            jumpCount = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (blockLayer == (blockLayer | (1 << layer)) && isDash)
        {
            isDash = false;
            rigid.DOKill();
        }
    }
}