using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    protected WeaponParent parent = null;
    public WeaponParent Parent { get => parent; set => parent = value; }
    
    #region 무기 정보 관련 변수
    public int weaponNumber = 0;
    [SerializeField] private WeaponSkill skill = null;
    [SerializeField] protected WeaponData data = null;
    public WeaponData Data => data;
    #endregion

    #region Element Marble
    [SerializeField]
    protected MarbleController marbleController;
    public MarbleController MarbleController => marbleController;

    protected float Damage => (data.damage + Player.AttackPlus) * Player.AttackWeight;
    //protected float Range => (1f + marbleController.PowerWeight * 0.01f) * data.range;
    protected float CoolTime => (1f - marbleController.SpeedWeight * 0.01f) * data.atkCool;
    #endregion

    protected WeaponSkill weaponSkill;
    public WeaponSkill WeaponSkill => weaponSkill;

    private void Awake()
    {
        if (!GameManager.Instance.SpreadData.IsLoading)
        {
            SetData();
        }
        else
        {
            EventManager.StartListening(Define.ON_END_READ_DATA, SetData);
        }
    }

    protected virtual void Start()
    {

    }

    public void SetData()
    {
        data = GameManager.Instance.SpreadData.GetData<WeaponData>(weaponNumber);
        marbleController = new MarbleController(gameObject);
    }

    //선 딜레이 시작
    public abstract void PreDelay();

    //선 딜레이 종료
    public abstract void HitTime();

    //후 딜레이 시작
    public abstract void PostDelay();

    //후 딜레이 종료
    public abstract void Stay();

    //공격을 강제로 종료 시킬 때
    public abstract void StopAttack();
    
    /// <summary>
    /// factor * 기본 범위만큼 범위가 time동안 증가한다. time이 0일 시 영구 증가.
    /// </summary>
    public abstract void BuffRange(float factor, float time = 0);

    /// <summary>
    /// 무기를 handle의 위치에 장착
    /// </summary>
    /// <returns>자기 자신을 리턴</returns>
    public WeaponScript Equip(Transform handle, bool isEnemy)
    {
        transform.SetParent(handle);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        data.isEnemy = isEnemy;
        gameObject.SetActive(true);
        return this;
    }

    public void AttackEnemey(GameObject enemy, int damage = -1)
    {
        int attackDamage = (damage < 0) ? (int)Damage : damage;

        if (enemy.tag == "CLOSE")
        {
            enemy.GetComponent<MeleeMonster>().GetDamage(10, 0, false, 0);
            //임시
            GameManager.Instance.UI.SpawnDamagePopup(enemy.transform, attackDamage, data.IsCritical);
            marbleController.ExecuteAttack(enemy.GetComponent<StateMachine>());
        }
        else if (enemy.tag == "FAR")
        {
            enemy.GetComponent<FarMonster>().GetDamage(10, 0, false, 0);
            //임시
            GameManager.Instance.UI.SpawnDamagePopup(enemy.transform, attackDamage, data.IsCritical);
            marbleController.ExecuteAttack(enemy.GetComponent<StateMachine>());
        }

        parent.GetComponent<CharacterHp>().Heal(attackDamage);
    }

    protected void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetData);
    }
}