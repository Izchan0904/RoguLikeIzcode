using TMPro;
using UnityEngine;

public class CharacterUpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI pendingCostText;

    void Update()
    {
        if (CharacterUpgradeSystem.Instance == null) return;

        hpText.text = "HP : " + CharacterUpgradeSystem.Instance.GetMaxHP();
        attackText.text = "Attack : " + CharacterUpgradeSystem.Instance.GetAttack();
        mpText.text = "MP : " + CharacterUpgradeSystem.Instance.GetMaxMP();

        if (pendingCostText != null)
        {
            pendingCostText.text =
                "確定時コスト : " + CharacterUpgradeSystem.Instance.pendingCost;
        }
    }
}