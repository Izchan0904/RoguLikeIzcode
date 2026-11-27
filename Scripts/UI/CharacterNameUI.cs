using TMPro;
using UnityEngine;

public class CharacterNameUI : MonoBehaviour
{
    [Header("名前表示")]
    public TextMeshProUGUI nameText;

    void Start()
    {
        if (PartyDataHolder.Instance == null)
        {
            Debug.LogError("PartyDataHolderがありません");
            return;
        }

        if (PartyDataHolder.Instance.selectedCharacters.Count == 0)
        {
            Debug.LogError("選択キャラがいません");
            return;
        }

        CharacterData character =
            PartyDataHolder.Instance.selectedCharacters[0];

        nameText.text = character.characterName;
    }
}