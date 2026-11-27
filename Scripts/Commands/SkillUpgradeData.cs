using System;

[Serializable]
public class SkillUpgradeData
{
    // ダメージに直接加算する値
    public int bonusDamage = 1;

    // 消費MPを減らす値
    public int bonusMpCostReduction = 0;

    // 確定時に消費する予定コイン
    public int pendingCost = 0;
}