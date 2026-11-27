using UnityEngine;
using AudioName;

public class SkillUpgradeButtonController : MonoBehaviour
{
    private SkillUpgradeSystem skillUpgradeSystem;
    private SkillData targetSkill;
    private SkillUpgradeType upgradeType;
    private CommandUpgradeSlotUI slotUI;

    public void Setup(
        SkillUpgradeSystem system,
        SkillData skill,
        SkillUpgradeType type,
        CommandUpgradeSlotUI ownerSlot)
    {
        skillUpgradeSystem = system;
        targetSkill = skill;
        upgradeType = type;
        slotUI = ownerSlot;
    }

    public void OnClickUpgrade()
    {
        if (skillUpgradeSystem == null)
        {
            Debug.LogError(
                "SkillUpgradeSystemが設定されていません"
            );
            return;
        }

        if (targetSkill == null)
        {
            Debug.LogError(
                "強化対象のSkillDataがありません"
            );
            return;
        }

        skillUpgradeSystem.ApplyUpgrade(
            targetSkill,
            upgradeType
        );

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(
                SEName.UPGRADE_SE_NAME
            );
        }

        if (slotUI != null)
        {
            slotUI.RefreshUI();
        }
    }
}