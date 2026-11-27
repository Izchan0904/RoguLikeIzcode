using AudioName;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// スタートボタンの制御を行うクラス
public class StartButtonController : MonoBehaviour
{
    public PartySelectManager partyManager;
    public Button startButton;
    public string nextSceneName = "BattleScene";

    void Update()
    {
        if (partyManager == null || startButton == null) return;

        startButton.interactable =
            partyManager.selectedCharacters.Count >= partyManager.maxPartySize;
    }

    public void OnClickStart()
    {
        if (PartyDataHolder.Instance == null)
        {
            Debug.LogError("がありません");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySE(
                SEName.GAMESTART_SE_NAME);
        }


        PartyDataHolder.Instance.selectedCharacters =
            new List<CharacterData>(partyManager.selectedCharacters);

        SceneManager.LoadScene(nextSceneName);
    }
}