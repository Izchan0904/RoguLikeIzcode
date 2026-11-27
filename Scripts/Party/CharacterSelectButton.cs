using UnityEngine;
using AudioName;
using UnityEngine.UI;

// キャラクター選択ボタンのクラス
public class CharacterSelectButton : MonoBehaviour
{
    public CharacterData characterData;
    public PartySelectManager partyManager;

    [SerializeField]
    private Button button;

    private void Start()
    {
        button.interactable = true;
    }

    public void OnClickSelect()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(
                SEName.BUTTON_SE_NAME);
        }

        button.interactable = false;

        partyManager.AddCharacter(characterData);
    }
}