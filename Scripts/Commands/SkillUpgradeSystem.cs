using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeSystem : MonoBehaviour
{
    [Header("スキルごとの強化情報")]
    public List<SkillUpgradeEntry> skills =
        new List<SkillUpgradeEntry>();

    [Header("1回の強化量")]
    public int damagePlusValue = 1;
    public int mpCostReductionPlusValue = 1;

    [Header("1回の強化コスト")]
    public int damageUpgradeCost = 50;
    public int mpCostReductionCost = 50;

    public void ApplyUpgrade(
        SkillData skill,
        SkillUpgradeType type)
    {
        Debug.Log("ApplyUpgrade : " + skill);

        if (skill == null)
        {
            Debug.LogError("強化対象のSkillDataがありません");
            return;
        }

        RegisterSkillIfNeeded(skill);

        SkillUpgradeEntry entry =
            GetEntry(skill);

        if (entry == null)
        {
            Debug.LogError("スキルの強化情報を取得できません");
            return;
        }

        switch (type)
        {
            case SkillUpgradeType.Damage:
                entry.upgrade.bonusDamage +=
                    damagePlusValue;

                entry.upgrade.pendingCost +=
                    damageUpgradeCost;
                break;

            case SkillUpgradeType.MpCost:
                entry.upgrade.bonusMpCostReduction +=
                    mpCostReductionPlusValue;

                entry.upgrade.pendingCost +=
                    mpCostReductionCost;
                break;
        }

        Debug.Log(
            skill.skillName +
            " ダメージ追加：" +
            GetBonusDamage(skill) +
            " MP軽減：" +
            entry.upgrade.bonusMpCostReduction
        );
    }

    public int GetBonusDamage(
        SkillData skill)
    {
        SkillUpgradeEntry entry =
            GetEntry(skill);

        if (entry == null ||
            entry.upgrade == null)
        {
            return 0;
        }

        return entry.upgrade.bonusDamage;
    }

    public int GetMpCost(
        SkillData skill)
    {
        if (skill == null)
        {
            return 0;
        }

        SkillUpgradeEntry entry =
            GetEntry(skill);

        if (entry == null ||
            entry.upgrade == null)
        {
            return skill.mpCost;
        }

        return Mathf.Max(
            0,
            skill.mpCost -
            entry.upgrade.bonusMpCostReduction
        );
    }

    public int GetTotalPendingCost()
    {
        int total = 0;

        foreach (SkillUpgradeEntry entry in skills)
        {
            if (entry == null ||
                entry.upgrade == null)
            {
                continue;
            }

            total += entry.upgrade.pendingCost;
        }

        return total;
    }

    public void ClearPendingCost()
    {
        foreach (SkillUpgradeEntry entry in skills)
        {
            if (entry == null ||
                entry.upgrade == null)
            {
                continue;
            }

            entry.upgrade.pendingCost = 0;
        }
    }

    public void LoadSavedSkillUpgrade(
        SkillData skill)
    {
        if (skill == null ||
            PartyDataHolder.Instance == null)
        {
            return;
        }

        RegisterSkillIfNeeded(skill);

        SkillUpgradeData saved =
            PartyDataHolder.Instance
                .GetSavedSkillUpgrade(skill);

        if (saved == null)
        {
            return;
        }

        SkillUpgradeEntry entry =
            GetEntry(skill);

        if (entry == null)
        {
            return;
        }

        entry.upgrade.bonusDamage =
            saved.bonusDamage;

        entry.upgrade.bonusMpCostReduction =
            saved.bonusMpCostReduction;

        entry.upgrade.pendingCost = 0;
    }

    public void RegisterSkillIfNeeded(
        SkillData skill)
    {
        if (skill == null)
        {
            return;
        }

        if (GetEntry(skill) != null)
        {
            return;
        }

        SkillUpgradeEntry newEntry =
            new SkillUpgradeEntry();

        newEntry.baseSkill = skill;
        newEntry.upgrade =
            new SkillUpgradeData();

        skills.Add(newEntry);
    }

    private SkillUpgradeEntry GetEntry(
        SkillData skill)
    {
        foreach (SkillUpgradeEntry entry in skills)
        {
            if (entry != null &&
                entry.baseSkill == skill)
            {
                return entry;
            }
        }

        return null;
    }
}