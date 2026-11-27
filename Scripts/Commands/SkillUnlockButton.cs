using UnityEngine;
using UnityEngine.UI;
using AudioName;

public class SkillUnlockButton : MonoBehaviour
{
    [Header("解放コスト")]
    public int unlockCost = 100;

    [Header("UI")]
    public Button unlockButton;
    public Text buttonText;
    public CommandUpgradeSlotUI slotUI;

    void Awake()
    {
        if (unlockButton == null)
        {
            unlockButton = GetComponent<Button>();
        }
    }

    void Update()
    {
        if (unlockButton == null) return;
        if (CoinManager.Instance == null) return;
        if (PartyDataHolder.Instance == null) return;
        if (slotUI == null) return;

        CharacterData character = GetTargetCharacter();
        SkillData skill = slotUI.GetCurrentSkill();

        if (character == null || skill == null) return;

        bool unlocked =
            PartyDataHolder.Instance.IsSkillUnlocked(character, skill)
            || character.skills.Contains(skill);

        unlockButton.interactable =
            !unlocked &&
            CoinManager.Instance.HasEnough(unlockCost);

        if (buttonText != null)
        {
            buttonText.text = unlockCost + "G 解放";
        }

        if (unlocked)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClickUnlock()
    {
        if (CoinManager.Instance == null) return;
        if (PartyDataHolder.Instance == null) return;
        if (slotUI == null) return;

        CharacterData character = GetTargetCharacter();
        SkillData skill = slotUI.GetCurrentSkill();

        if (character == null || skill == null) return;

        if (PartyDataHolder.Instance.IsSkillUnlocked(character, skill))
        {
            gameObject.SetActive(false);
            return;
        }

        if (!CoinManager.Instance.SpendCoin(unlockCost))
        {
            Debug.Log("コイン不足");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(SEName.CONFIRM_SE_NAME);
        }

        PartyDataHolder.Instance.UnlockSkill(character, skill);

        SaveManager.Instance.Save();

        if (slotUI != null)
        {
            slotUI.RefreshUI();
        }

        gameObject.SetActive(false);

        Debug.Log(skill.skillName + " を解放しました");
    }

    CharacterData GetTargetCharacter()
    {
        if (PartyDataHolder.Instance == null) return null;
        if (PartyDataHolder.Instance.selectedCharacters.Count == 0) return null;

        return PartyDataHolder.Instance.selectedCharacters[0];
    }
}