using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonController : MonoBehaviour
{
    [Header("戻る先シーン")]
    public string sceneName = "CharacterSelectScene";

    public void ReturnToCharacterSelect()
    {
        SceneManager.LoadScene(sceneName);
    }
}