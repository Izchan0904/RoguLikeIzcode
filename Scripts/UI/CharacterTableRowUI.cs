using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTableRowUI : MonoBehaviour
{
    [Header("画像")]
    public Image characterImage;

    [Header("テキスト")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI skillText;

    public void Setup(CharacterData character)
    {
        if (character == null) return;

        if (characterImage != null)
        {
            characterImage.sprite = character.characterSprite;
            characterImage.gameObject.SetActive(true);
        }

        if (nameText != null)
        {
            nameText.text = character.characterName;
        }

        if (hpText != null)
        {
            hpText.text = "HP : " + character.maxHp;
        }

        if (mpText != null)
        {
            mpText.text = "MP : " + character.maxMp;
        }

        if (attackText != null)
        {
            attackText.text = "Attack : " + character.attackPower;
        }

        if (skillText != null)
        {
            string skills = "";

            for (int i = 0; i < character.skills.Count; i++)
            {
                if (character.skills[i] == null) continue;

                skills += character.skills[i].skillName;

                if (i < character.skills.Count - 1)
                {
                    skills += " / ";
                }
            }

            skillText.text = skills;
        }
    }
}