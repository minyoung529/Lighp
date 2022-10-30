using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : CharacterSkill
{
    // TEST
    public bool isOne = false;

    protected override void Awake()
    {
        base.Awake();
        EventManager.StartListening(Define.ON_END_READ_DATA, AddFirstSkill);
    }

    protected override void Update()
    {
        if (InputManager.GetKeyDown(InputAction.ActiveSkill))
        {
            ExecuteCurrentSkill(0);
        }

        // TEST
        if(!isOne)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExecuteCurrentSkill(1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ExecuteCurrentSkill(2);
            }
        }
        
        base.Update();
    }

    // TEST
    private void AddFirstSkill()
    {
        Skill overclock = GameManager.Instance.GetSkill<Overclock>();
        AddSkill(overclock);
        GameManager.Instance.UI.Skill.RegisterSkill(overclock);

        if(!isOne)
        {
            Skill flashburst = GameManager.Instance.GetSkill<FlashBurst>();
            Skill portableCharger = GameManager.Instance.GetSkill<PortableCharger>();

            AddSkill(flashburst);
            AddSkill(portableCharger);

            GameManager.Instance.UI.Skill.RegisterSkill(flashburst, 1);
            GameManager.Instance.UI.Skill.RegisterSkill(portableCharger, 2);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, AddFirstSkill);
    }
}
