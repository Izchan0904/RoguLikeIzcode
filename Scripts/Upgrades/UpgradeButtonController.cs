using UnityEngine;
using UnityEngine.UI;
using AudioName;

public class UpgradeButtonController : MonoBehaviour
{
    [Header("強化するステータス")]
    public UpgradeType upgradeType;

    [Header("ボタン")]
    public Button upgradeButton;

    void Awake()
    {
        if (upgradeButton == null)
        {
            upgradeButton = GetComponent<Button>();
        }
    }

    void Update()
    {
        if (upgradeButton == null) return;
        if (CharacterUpgradeSystem.Instance == null) return;
        if (CoinManager.Instance == null) return;

        int nextCost =
            CharacterUpgradeSystem.Instance.GetUpgradeCost(
                upgradeType);

        int totalCost =
            CharacterUpgradeSystem.Instance.pendingCost +
            nextCost;

        upgradeButton.interactable =
            CoinManager.Instance.HasEnough(totalCost);
    }

    public void OnClickUpgrade()
    {
        if (CharacterUpgradeSystem.Instance == null) return;
        if (CoinManager.Instance == null) return;

        int nextCost =
            CharacterUpgradeSystem.Instance.GetUpgradeCost(
                upgradeType);

        int totalCost =
            CharacterUpgradeSystem.Instance.pendingCost +
            nextCost;

        if (!CoinManager.Instance.HasEnough(totalCost))
        {
            Debug.Log("コイン不足で強化できません");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(
                SEName.UPGRADE_SE_NAME);
        }

        CharacterUpgradeSystem.Instance.ApplyUpgrade(upgradeType);
    }
}