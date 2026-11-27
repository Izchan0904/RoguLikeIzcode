using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterUpgradeSaveData
{
    public int characterId;

    public int bonusHp;
    public int bonusPowerMultiplier;
    public int bonusMp;
}

public class PartyDataHolder : MonoBehaviour
{
    public static PartyDataHolder Instance;

    [Header("強化済み複製スキル")]
    public List<RuntimeSkillEntry> runtimeSkills =
    new List<RuntimeSkillEntry>();

    [Header("選択中のキャラクター")]
    public List<CharacterData> selectedCharacters =
        new List<CharacterData>();

    [Header("キャラクターごとの強化値")]
    public List<CharacterUpgradeSaveData> savedCharacterUpgrades =
        new List<CharacterUpgradeSaveData>();

    [Header("技ごとの強化値")]
    public List<SkillUpgradeEntry> savedSkillUpgrades =
        new List<SkillUpgradeEntry>();

    [Header("解放済みの技")]
    public List<UnlockedSkillData> unlockedSkills =
        new List<UnlockedSkillData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// キャラクターの強化値を保存する
    /// </summary>
    public void SaveCharacterUpgrade(
        CharacterData character,
        CharacterUpgradeData upgradeData)
    {
        if (character == null)
        {
            Debug.LogError(
                "SaveCharacterUpgrade：CharacterDataがありません"
            );

            return;
        }

        if (upgradeData == null)
        {
            Debug.LogError(
                "SaveCharacterUpgrade：強化データがありません"
            );

            return;
        }

        CharacterUpgradeSaveData savedData =
            savedCharacterUpgrades.Find(
                data =>
                    data.characterId ==
                    character.characterId
            );

        // まだ保存されていないキャラなら追加
        if (savedData == null)
        {
            savedData =
                new CharacterUpgradeSaveData();

            savedData.characterId =
                character.characterId;

            savedCharacterUpgrades.Add(savedData);
        }

        savedData.bonusHp =
            upgradeData.bonusHp;

        savedData.bonusPowerMultiplier =
            upgradeData.bonusAttack;

        savedData.bonusMp =
            upgradeData.bonusMp;

        Debug.Log(
            character.characterName +
            " の強化値を保存しました" +
            " HP+" + savedData.bonusHp +
            " ATK+" + savedData.bonusPowerMultiplier +
            " MP+" + savedData.bonusMp
        );
    }

    /// <summary>
    /// キャラクターの保存済み強化値を取得する
    /// </summary>
    public CharacterUpgradeSaveData GetCharacterUpgrade(
        CharacterData character)
    {
        if (character == null)
        {
            return null;
        }

        return savedCharacterUpgrades.Find(
            data =>
                data.characterId ==
                character.characterId
        );
    }

    /// <summary>
    /// 強化後の最大HPを取得する
    /// </summary>
    public int GetCharacterHp(
        CharacterData character)
    {
        if (character == null)
        {
            return 0;
        }

        CharacterUpgradeSaveData upgrade =
            GetCharacterUpgrade(character);

        if (upgrade == null)
        {
            return character.maxHp;
        }

        return character.maxHp +
               upgrade.bonusHp;
    }

    /// <summary>
    /// 強化後の攻撃力を取得する
    /// </summary>
    public int GetCharacterAttack(
        CharacterData character)
    {
        if (character == null)
        {
            return 0;
        }

        CharacterUpgradeSaveData upgrade =
            GetCharacterUpgrade(character);

        if (upgrade == null)
        {
            return character.attackPower;
        }

        return character.attackPower +
               upgrade.bonusPowerMultiplier;
    }

    /// <summary>
    /// 強化後の最大MPを取得する
    /// </summary>
    public int GetCharacterMp(
        CharacterData character)
    {
        if (character == null)
        {
            return 0;
        }

        CharacterUpgradeSaveData upgrade =
            GetCharacterUpgrade(character);

        if (upgrade == null)
        {
            return character.maxMp;
        }

        return character.maxMp +
               upgrade.bonusMp;
    }

    /// <summary>
    /// 指定キャラクターの強化値を削除する
    /// </summary>
    public void ResetCharacterUpgrade(
        CharacterData character)
    {
        CharacterUpgradeSaveData savedData =
            GetCharacterUpgrade(character);

        if (savedData != null)
        {
            savedCharacterUpgrades.Remove(savedData);
        }
    }

    /// <summary>
    /// 全キャラクターの強化値を削除する
    /// </summary>
    public void ResetAllCharacterUpgrades()
    {
        savedCharacterUpgrades.Clear();
    }

    /// <summary>
    /// GetSavedUpgradeと同じ
    /// </summary>
    public SkillUpgradeData GetSavedUpgrade(
      SkillData skill)
    {
        if (skill == null)
        {
            Debug.LogError(
                "GetSavedUpgrade：SkillDataがありません"
            );

            return null;
        }

        SkillUpgradeEntry entry =
            savedSkillUpgrades.Find(
                data => data.baseSkill == skill
            );

        if (entry == null)
        {
            entry = new SkillUpgradeEntry();

            entry.baseSkill = skill;
            entry.upgrade = new SkillUpgradeData();

            savedSkillUpgrades.Add(entry);
        }

        return entry.upgrade;
    }

    /// <summary>
    /// 技の強化データがすでに保存されているか
    /// </summary>
    public bool HasSavedSkillUpgrade(
        SkillData skill)
    {
        if (skill == null)
        {
            return false;
        }

        return savedSkillUpgrades.Exists(
            data => data.baseSkill == skill
        );
    }
    /// <summary>
    /// 指定キャラクターの技が解放済みか確認する
    /// </summary>
    public bool IsSkillUnlocked(
        CharacterData character,
        SkillData skill)
    {
        if (character == null ||
            skill == null)
        {
            return false;
        }

        return unlockedSkills.Exists(
            data =>
                data.character == character &&
                data.skill == skill
        );
    }

    /// <summary>
    /// キャラクター指定なしで技が解放済みか確認する
    /// 既存コードとの互換用
    /// </summary>
    public bool IsSkillUnlocked(
        SkillData skill)
    {
        if (skill == null)
        {
            return false;
        }

        return unlockedSkills.Exists(
            data => data.skill == skill
        );
    }

    /// <summary>
    /// キャラクターの技を解放する
    /// </summary>
    public void UnlockSkill(
        CharacterData character,
        SkillData skill)
    {
        if (character == null ||
            skill == null)
        {
            Debug.LogError(
                "UnlockSkill：キャラクターまたは技がありません"
            );

            return;
        }

        if (IsSkillUnlocked(character, skill))
        {
            return;
        }

        UnlockedSkillData unlockedData =
            new UnlockedSkillData();

        unlockedData.character = character;
        unlockedData.skill = skill;

        unlockedSkills.Add(unlockedData);

        Debug.Log(
            character.characterName +
            " が " +
            skill.skillName +
            " を解放しました"
        );
    }

    /// <summary>
    /// キャラクターが解放している技を取得する
    /// </summary>
    public List<SkillData> GetUnlockedSkills(
        CharacterData character)
    {
        List<SkillData> result =
            new List<SkillData>();

        if (character == null)
        {
            return result;
        }

        foreach (
            UnlockedSkillData unlocked
            in unlockedSkills)
        {
            if (unlocked.character == character &&
                unlocked.skill != null)
            {
                result.Add(unlocked.skill);
            }
        }

        return result;
    }

    /// <summary>
    /// 技の強化データを保存する
    /// </summary>
    public void SaveSkillUpgrades(
        List<SkillUpgradeEntry> skillUpgrades)
    {
        Debug.Log("===== SaveSkillUpgrades =====");

        foreach (SkillUpgradeEntry entry in skillUpgrades)
        {
            Debug.Log(
                "entry=" + entry +
                " baseSkill=" + entry?.baseSkill +
                " upgrade=" + entry?.upgrade
            );
        }

        if (skillUpgrades == null)
        {
            Debug.LogError(
                "SaveSkillUpgrades：技強化データがありません"
            );

            return;
        }

        foreach (SkillUpgradeEntry newEntry in skillUpgrades)
        {
            if (newEntry == null ||
                newEntry.baseSkill == null ||
                newEntry.upgrade == null)
            {
                continue;
            }

            SkillUpgradeEntry savedEntry =
                savedSkillUpgrades.Find(
                    entry =>
                        entry.baseSkill ==
                        newEntry.baseSkill
                );

            // まだ保存されていない技なら新しく追加
            if (savedEntry == null)
            {
                savedEntry =
                    new SkillUpgradeEntry();

                savedEntry.baseSkill =
                    newEntry.baseSkill;

                savedEntry.upgrade =
                    new SkillUpgradeData();

                savedSkillUpgrades.Add(savedEntry);
            }

            // 強化値をコピー
            savedEntry.upgrade.bonusDamage =
     newEntry.upgrade.bonusDamage;

            savedEntry.upgrade.bonusMpCostReduction =
                newEntry.upgrade.bonusMpCostReduction;

            // 確定後なので保留コストは保存しない
            savedEntry.upgrade.pendingCost = 0;
        }

        Debug.Log("技の強化データを保存しました");
    }

    /// <summary>
    /// キャラクターの保存済み強化値を取得する
    /// </summary>
    public CharacterUpgradeData GetSavedUpgrade(
        CharacterData character)
    {
        CharacterUpgradeData result =
            new CharacterUpgradeData();

        if (character == null)
        {
            return result;
        }

        CharacterUpgradeSaveData savedData =
            GetCharacterUpgrade(character);

        if (savedData == null)
        {
            return result;
        }

        result.bonusHp =
            savedData.bonusHp;

        result.bonusAttack =
            savedData.bonusPowerMultiplier;

        result.bonusMp =
            savedData.bonusMp;

        return result;
    }

    /// <summary>
    /// 技の保存済み強化値を取得する
    /// </summary>
    public SkillUpgradeData GetSavedSkillUpgrade(
        SkillData skill)
    {
        return GetSavedUpgrade(skill);
    }

    public void SaveRuntimeSkill(
    CharacterData character,
    SkillData runtimeSkill)
    {
        if (character == null)
        {
            Debug.LogWarning(
                "保存するキャラクターがありません"
            );

            return;
        }

        if (runtimeSkill == null)
        {
            Debug.LogWarning(
                "保存するスキルがありません"
            );

            return;
        }

        RuntimeSkillEntry foundEntry =
            runtimeSkills.Find(entry =>
                entry.characterId ==
                    character.characterId &&
                entry.skillId ==
                    runtimeSkill.skillId
            );

        if (foundEntry == null)
        {
            foundEntry =
                new RuntimeSkillEntry();

            foundEntry.characterId =
                character.characterId;

            foundEntry.skillId =
                runtimeSkill.skillId;

            runtimeSkills.Add(foundEntry);
        }

        // 古い複製データがあれば破棄
        if (foundEntry.runtimeSkill != null)
        {
            Destroy(
                foundEntry.runtimeSkill
            );
        }

        // 強化後のSkillDataを複製して保存
        foundEntry.runtimeSkill =
            Instantiate(runtimeSkill);

        foundEntry.runtimeSkill.name =
            runtimeSkill.name + "_Saved";

        Debug.Log(
            character.characterName +
            "の「" +
            runtimeSkill.skillName +
            "」を保存しました" +
            " 威力：" +
            runtimeSkill.powerMultiplier +
            " MP：" +
            runtimeSkill.mpCost
        );
    }

    public SkillData GetRuntimeSkill(
        int characterId,
        int skillId)
    {
        RuntimeSkillEntry foundEntry =
            runtimeSkills.Find(entry =>
                entry.characterId ==
                    characterId &&
                entry.skillId ==
                    skillId
            );

        if (foundEntry == null)
        {
            return null;
        }

        return foundEntry.runtimeSkill;
    }

    public SkillData GetBattleSkill(
        CharacterData character,
        SkillData originalSkill)
    {
        if (character == null ||
            originalSkill == null)
        {
            return originalSkill;
        }

        SkillData savedSkill =
            GetRuntimeSkill(
                character.characterId,
                originalSkill.skillId
            );

        if (savedSkill != null)
        {
            return savedSkill;
        }

        return originalSkill;
    }

    public void ClearRuntimeSkills(
        int characterId)
    {
        for (int i = runtimeSkills.Count - 1;
             i >= 0;
             i--)
        {
            RuntimeSkillEntry entry =
                runtimeSkills[i];

            if (entry.characterId !=
                characterId)
            {
                continue;
            }

            if (entry.runtimeSkill != null)
            {
                Destroy(
                    entry.runtimeSkill
                );
            }

            runtimeSkills.RemoveAt(i);
        }
    }
}