using UnityEngine;

/// <summary>
/// 스킬의 상위 클래스
/// </summary>
[System.Serializable]
public abstract class Skill
{
    #region SET_IN_SPREAD_SHEET
    public int number;
    protected string skillName;

    protected float duration;
    public float coolTime;

    protected float costValue;
    protected float rewardValue;

    protected string info;
    #endregion

    protected Character character;

    /// <summary>
    /// 스킬이 사용 중인지 아닌지를 판별
    /// </summary>
    public bool IsUsing { get; set; } = false;

    /// <summary>
    /// 스킬을 사용할 수 있는지 없는지를 판별
    /// </summary>
    public bool CanUseSkill { get; set; } = true;

    private float coolTimer = 0f;
    public float CoolTimer => coolTimer;

    private float skillTimer = 0f;
    // 1초마다 한번씩 돌아가는 타이머
    private float secondTimer = 0f;

    public void Init(Character character)
    {
        this.character = character;
        OnAwake();
    }

    /// <summary>
    /// 시작할 때 딱 한 번만 실행되는 함수
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// 스킬을 시작할 때 호출하는 함수
    /// </summary>
    public void Start()
    {
        IsUsing = true;
        CanUseSkill = false;
        Execute();
    }

    /// <summary>
    /// 스킬 능력 사용시 구현하는 함수
    /// </summary>
    protected abstract void Execute();

    public void Update()
    {
        if (IsUsing)
        {
            skillTimer += Time.deltaTime;
            secondTimer += Time.deltaTime;

            if (skillTimer > duration)
            {
                End();
               
                return;
            }
            else if (secondTimer > 1f)
            {
                UpdatePerSecond();
                secondTimer = 0f;
            }

            OnUpdate();
        }
        else
        {
            if (coolTimer < 0f)
            {
                CanUseSkill = true;
            }
            else
            {
                coolTimer -= Time.deltaTime;
            }
        }
    }


    protected virtual void OnUpdate() { }

    /// <summary>
    /// 스킬 사용 중 1초마다 한번씩 실행되는 함수
    /// </summary>
    protected virtual void UpdatePerSecond() { }

    protected virtual void OnEnd() { }

    private void End()
    {
        IsUsing = false;
        coolTimer = coolTime;
        secondTimer = 0f;
        skillTimer = 0f;
        OnEnd();
    }


}