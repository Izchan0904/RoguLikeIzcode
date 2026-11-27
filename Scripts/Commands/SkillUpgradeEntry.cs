using UnityEngine;

[System.Serializable]
public class SkillUpgradeEntry
{
    [Header("強化対象のキャラクター")]
    public int characterId;

    [Header("強化対象の元スキル")]
    public SkillData baseSkill;

    [Header("スキル強化内容")]
    public SkillUpgradeData upgrade =
        new SkillUpgradeData();
}