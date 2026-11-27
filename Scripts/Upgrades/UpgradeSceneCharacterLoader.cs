using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneCharacterLoader : MonoBehaviour
{
    [Header("表示UI")]
    public Image characterImage;
    public TextMeshProUGUI nameText;

    void Start()
    {
        if (PartyDataHolder.Instance == null) return;
        if (PartyDataHolder.Instance.selectedCharacters.Count == 0) return;
        if (CharacterUpgradeSystem.Instance == null) return;

        CharacterData firstCharacter =
            PartyDataHolder.Instance.selectedCharacters[0];

        CharacterUpgradeSystem.Instance.SetCharacter(firstCharacter);

        if (characterImage != null)
        {
            characterImage.sprite = firstCharacter.characterSprite;
            characterImage.gameObject.SetActive(true);
        }

        if (nameText != null)
        {
            nameText.text = firstCharacter.characterName;
        }
    }
}