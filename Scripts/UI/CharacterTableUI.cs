using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardUI : MonoBehaviour
{
    [Header("表示するキャラ")]
    public CharacterData characterData;

    [Header("画像")]
    public Image characterImage;

    [Header("テキスト")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI skillText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (characterData == null) return;

        if (characterImage != null)
        {
            characterImage.sprite = characterData.characterSprite;
        }

        if (nameText != null)
        {
            nameText.text = characterData.characterName;
        }

        int hp = characterData.maxHp;
        int mp = characterData.maxMp;
        int attack = characterData.attackPower;

        if (PartyDataHolder.Instance != null)
        {
            CharacterUpgradeData saved =
                PartyDataHolder.Instance.GetSavedUpgrade(characterData);

            if (saved != null)
            {
                hp += saved.bonusHp;
                mp += saved.bonusMp;
                attack += saved.bonusAttack;
            }
        }

        if (hpText != null)
        {
            hpText.text = "HP : " + hp;
        }

        if (mpText != null)
        {
            mpText.text = "MP : " + mp;
        }

        if (attackText != null)
        {
            attackText.text = "ATK : " + attack;
        }

        if (skillText != null)
        {
            string skills = "";

            for (int i = 0; i < characterData.skills.Count; i++)
            {
                if (characterData.skills[i] == null) continue;

                skills += characterData.skills[i].skillName;

                if (i < characterData.skills.Count - 1)
                {
                    skills += "\n";
                }
            }

            skillText.text = skills;
        }
    }
}