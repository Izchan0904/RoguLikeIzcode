using System.Collections.Generic;
using UnityEngine;

public class CommandUpgradeListUI : MonoBehaviour
{
    [Header("スキル強化システム")]
    public SkillUpgradeSystem skillUpgradeSystem;

    [Header("表示スロット")]
    public CommandUpgradeSlotUI[] slots;

    private void Start()
    {
        ShowSelectedCharacterCommands();
    }

    public void ShowSelectedCharacterCommands()
    {
        if (PartyDataHolder.Instance == null)
        {
            Debug.LogError("PartyDataHolder がありません");
            return;
        }

        if (PartyDataHolder.Instance.selectedCharacters == null ||
            PartyDataHolder.Instance.selectedCharacters.Count == 0)
        {
            Debug.LogError("選択キャラクターがいません");
            return;
        }

        if (skillUpgradeSystem == null)
        {
            Debug.LogError("SkillUpgradeSystem が未設定です");
            return;
        }

        CharacterData character =
            PartyDataHolder.Instance.selectedCharacters[0];

        List<SkillData> displaySkills =
            new List<SkillData>();

        if (character.skills != null)
        {
            foreach (SkillData skill in character.skills)
            {
                if (skill != null &&
                    !displaySkills.Contains(skill))
                {
                    displaySkills.Add(skill);
                }
            }
        }

        if (character.unlockableSkills != null)
        {
            foreach (SkillData skill in character.unlockableSkills)
            {
                if (skill != null &&
                    !displaySkills.Contains(skill))
                {
                    displaySkills.Add(skill);
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                continue;
            }

            if (i < displaySkills.Count)
            {
                SkillData skill = displaySkills[i];

                skillUpgradeSystem.RegisterSkillIfNeeded(skill);
                skillUpgradeSystem.LoadSavedSkillUpgrade(skill);

                slots[i].Setup(
                    character,
                    skill,
                    skillUpgradeSystem
                );
            }
        }
    }
}