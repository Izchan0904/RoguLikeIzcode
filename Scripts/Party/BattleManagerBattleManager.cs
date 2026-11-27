using UnityEngine;

//バトルシーンに選択したキャラクターの情報を表示するためのクラス

public class BattleManager : MonoBehaviour
{
    void Start()
    {
        
        var party = PartyDataHolder.Instance.selectedCharacters;

        foreach (var c in party)
        {
            Debug.Log(c.characterName);
        }
    }
}