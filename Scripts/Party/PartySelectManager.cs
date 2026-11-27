using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySelectManager : MonoBehaviour
{
    public List<CharacterData> selectedCharacters =
        new List<CharacterData>();

    public int maxPartySize = 4;

    [Header("パーティー画像")]
    public Image[] partyImages;

    [Header("選択中キャラのプレビュー画像")]
    public Image previewImage;

    [SerializeField]
    private Button[] partyButton;

    void Start()
    {
        for (int i = 0; i < partyImages.Length; i++)
        {
            partyImages[i].gameObject.SetActive(false);
        }

        if (previewImage != null)
        {
            previewImage.gameObject.SetActive(false);
        }
    }

    public void AddCharacter(CharacterData character)
    {
        if (selectedCharacters.Count >= maxPartySize)
        {
            Debug.Log("これ以上選べない");
            return;
        }

        selectedCharacters.Add(character);

        int index = selectedCharacters.Count - 1;

        partyImages[index].sprite = character.characterSprite;
        partyImages[index].gameObject.SetActive(true);

        if (previewImage != null)
        {
            previewImage.sprite = character.characterSprite;
            previewImage.gameObject.SetActive(true);
        }

        Debug.Log(character.characterName + " を選択");
    }

    public void ResetSelection()
    {
        selectedCharacters.Clear();

        for (int i = 0; i < partyImages.Length; i++)
        {
            partyImages[i].sprite = null;
            partyImages[i].gameObject.SetActive(false);
        }

        if (previewImage != null)
        {
            previewImage.sprite = null;
            previewImage.gameObject.SetActive(false);
        }

        foreach(Button button in partyButton)
        {
            if (!button.interactable)
            {
                button.interactable = true;
            }
        }

        Debug.Log("リセット");
    }
}