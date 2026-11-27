using UnityEngine;

[System.Serializable]
public class RuntimeSkillEntry
{
    [Header("所有キャラクターID")]
    public int characterId;

    [Header("元スキルID")]
    public int skillId;

    [Header("強化済みの複製スキル")]
    public SkillData runtimeSkill;
}