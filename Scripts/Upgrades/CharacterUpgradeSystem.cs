using UnityEngine;

public class CharacterUpgradeSystem : MonoBehaviour
{
    public static CharacterUpgradeSystem Instance;

    [Header("元データ")]
    public CharacterData baseData;

    [Header("強化データ")]
    public CharacterUpgradeData upgrade = new CharacterUpgradeData();

    [Header("強化で増える数値")]
    public int hpPlusValue = 10;
    public int attackPlusValue = 2;
    public int mpPlusValue = 5;

    [Header("1回の強化コスト")]
    public int hpCost = 50;
    public int attackCost = 50;
    public int mpCost = 50;

    [Header("確定時に消費する予定コイン")]
    public int pendingCost = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public int GetMaxHP()
    {
        if (baseData == null) return 0;
        return baseData.maxHp + upgrade.bonusHp;
    }

    public int GetAttack()
    {
        if (baseData == null) return 0;
        return baseData.attackPower + upgrade.bonusAttack;
    }

    public int GetMaxMP()
    {
        if (baseData == null) return 0;
        return baseData.maxMp + upgrade.bonusMp;
    }

    public void ApplyUpgrade(UpgradeType type)
    {
        Debug.Log("押されたタイプ = " + type);

        switch (type)
        {
            case UpgradeType.HP:
                upgrade.bonusHp += hpPlusValue;
                pendingCost += hpCost;
                break;

            case UpgradeType.Attack:
                upgrade.bonusAttack += attackPlusValue;
                pendingCost += attackCost;
                break;

            case UpgradeType.MP:
                upgrade.bonusMp += mpPlusValue;
                pendingCost += mpCost;
                break;
        }

        Debug.Log("仮強化: " + type);
        Debug.Log("確定時コスト: " + pendingCost);
    }

    public void ResetUpgrade()
    {
        upgrade.bonusHp = 0;
        upgrade.bonusAttack = 0;
        upgrade.bonusMp = 0;
        pendingCost = 0;
    }

    public int GetUpgradeCost(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.HP:
                return hpCost;

            case UpgradeType.Attack:
                return attackCost;

            case UpgradeType.MP:
                return mpCost;
        }

        return 0;
    }
    public void SetCharacter(CharacterData character)
    {
        baseData = character;

        pendingCost = 0;

        upgrade = new CharacterUpgradeData();

        if (PartyDataHolder.Instance == null)
        {
            return;
        }

        CharacterUpgradeData saved =
            PartyDataHolder.Instance
                .GetSavedUpgrade(character);

        if (saved != null)
        {
            upgrade.bonusHp =
                saved.bonusHp;

            upgrade.bonusAttack =
                saved.bonusAttack;

            upgrade.bonusMp =
                saved.bonusMp;
        }

        Debug.Log(
            character.characterName +
            " の強化データ読込"
        );
    }
}