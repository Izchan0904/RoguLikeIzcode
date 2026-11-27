using UnityEngine;
using AudioName;

public class ConfirmUpgradeButton : MonoBehaviour
{
    [Header("コマンド強化システム")]
    public SkillUpgradeSystem skillUpgradeSystem;

    public void OnClickConfirm()
    {
        if (CoinManager.Instance == null)
        {
            Debug.LogError("CoinManager がありません");
            return;
        }

        if (PartyDataHolder.Instance == null)
        {
            Debug.LogError("PartyDataHolder がありません");
            return;
        }

        int characterCost = 0;
        int skillCost = 0;

        if (CharacterUpgradeSystem.Instance != null)
        {
            characterCost =
                CharacterUpgradeSystem.Instance.pendingCost;
        }

        if (skillUpgradeSystem != null)
        {
            skillCost =
                skillUpgradeSystem.GetTotalPendingCost();
        }

        int totalCost = characterCost + skillCost;

        if (totalCost <= 0)
        {
            Debug.Log("強化内容がありません");
            return;
        }

        if (!CoinManager.Instance.SpendCoin(totalCost))
        {
            Debug.Log("コイン不足");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(
                SEName.CONFIRM_SE_NAME);
        }

        if (CharacterUpgradeSystem.Instance != null)
        {
            PartyDataHolder.Instance.SaveCharacterUpgrade(
                CharacterUpgradeSystem.Instance.baseData,
                CharacterUpgradeSystem.Instance.upgrade
            );
            SaveManager.Instance.Save();

            CharacterUpgradeSystem.Instance.pendingCost = 0;
        }

        if (skillUpgradeSystem != null)
        {
            PartyDataHolder.Instance.SaveSkillUpgrades(
                skillUpgradeSystem.skills
            );
            SaveManager.Instance.Save();

            skillUpgradeSystem.ClearPendingCost();
        }

        Debug.Log("強化確定 コイン消費: " + totalCost);

        if (skillUpgradeSystem != null)
        {
            PartyDataHolder.Instance.SaveSkillUpgrades(
                skillUpgradeSystem.skills
            );

            foreach (SkillUpgradeEntry entry in skillUpgradeSystem.skills)
            {
                if (entry == null ||
                    entry.baseSkill == null ||
                    entry.upgrade == null)
                {
                    continue;
                }

                Debug.Log(
                    "保存確認：" +
                    entry.baseSkill.skillName +
                    " / Damage追加：" +
                    entry.upgrade.bonusDamage +
                    " / MP軽減：" +
                    entry.upgrade.bonusMpCostReduction
            
                );
            }

            skillUpgradeSystem.ClearPendingCost();
        }
    }
}