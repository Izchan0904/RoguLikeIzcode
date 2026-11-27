using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSceneButtonController : MonoBehaviour
{
    public PartySelectManager partyManager;

    public Button upgradeButton;

    public string upgradeSceneName = "CharacterUpgradeScene";

    void Update()
    {
        if (partyManager == null || upgradeButton == null)
        {
            return;
        }

        upgradeButton.interactable =
            partyManager.selectedCharacters.Count > 0;
    }

    public void OnClickUpgradeScene()
    {
        if (PartyDataHolder.Instance == null)
        {
            Debug.LogError("PartyDataHolderがありません");
            return;
        }

        PartyDataHolder.Instance.selectedCharacters =
            new List<CharacterData>(
                partyManager.selectedCharacters);

        SceneManager.LoadScene(upgradeSceneName);
    }
}