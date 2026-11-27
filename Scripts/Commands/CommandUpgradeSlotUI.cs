using TMPro;
using UnityEngine;

public class CommandUpgradeSlotUI : MonoBehaviour
{
    [Header("表示テキスト")]
    public TMP_Text skillNameText;
    public TMP_Text damageText;
    public TMP_Text mpText;

    [Header("強化ボタン")]
    public SkillUpgradeButtonController damageButton;
    public SkillUpgradeButtonController mpButton;

    private CharacterData ownerCharacter;
    private SkillData currentSkill;
    private SkillUpgradeSystem currentSystem;

    public void Setup(
        CharacterData character,
        SkillData skill,
        SkillUpgradeSystem system)
    {
        ownerCharacter = character;
        currentSkill = skill;
        currentSystem = system;

        gameObject.SetActive(true);

        if (damageButton != null)
        {
            damageButton.Setup(
                system,
                skill,
                SkillUpgradeType.Damage,
                this
            );
        }

        if (mpButton != null)
        {
            mpButton.Setup(
                system,
                skill,
                SkillUpgradeType.MpCost,
                this
            );
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (currentSkill == null ||
            currentSystem == null)
        {
            return;
        }

        if (skillNameText != null)
        {
            skillNameText.text =
                string.IsNullOrEmpty(
                    currentSkill.skillName)
                ? currentSkill.name
                : currentSkill.skillName;
        }

        if (damageText != null)
        {
            damageText.text =
                "DAMAGE : +" +
                currentSystem.GetBonusDamage(
                    currentSkill
                );
        }

        if (mpText != null)
        {
            mpText.text =
                "MP : " +
                currentSystem.GetMpCost(
                    currentSkill
                );
        }
    }

    public SkillData GetCurrentSkill()
    {
        return currentSkill;
    }

    public CharacterData GetOwnerCharacter()
    {
        return ownerCharacter;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}