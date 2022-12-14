using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIController : MonoBehaviour
{
    [SerializeField]
    private Transform skillsParent;

    private List<SkillPanel> skillPanels;
    [SerializeField]
    private Sprite[] skillIcons;

    private void Awake()
    {
        skillPanels = new List<SkillPanel>(skillsParent.GetComponentsInChildren<SkillPanel>());
        skillIcons = Resources.LoadAll<Sprite>("Sprites/SkillIconTest");
    }

    private void OnEnable()
    {
        EventManager.StartListening(Define.ON_END_READ_DATA, ShowSkillPanels);
    }

    private void ShowSkillPanels()
    {
        int count = FindObjectOfType<CharacterSkill>().SkillCount;
        ShowPanels(count);
    }

    public void RegisterSkill(Skill skill, int index = 0)
    {
        if (skill == null) return;

        if (skillPanels.Count <= index) return;

        skillPanels[index].InitSkill(skill, skillIcons[skill.number - 1]);
    }

    private void ShowPanels(int count)
    {
        for (int i = 0; i < skillPanels.Count; i++)
        {
            skillPanels[i].gameObject.SetActive(i < count);
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, ShowSkillPanels);
    }
}
