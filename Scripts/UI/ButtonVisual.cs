using UnityEngine;
using UnityEngine.UI;

public class ButtonVisual : MonoBehaviour
{
    [Header("ボタン")]
    public Button targetButton;

    [Header("ボタン画像")]
    public Image targetImage;

    [Header("色設定")]
    public Color enableColor = Color.white;
    public Color disableColor = Color.gray;

    void Awake()
    {
        // 自動取得
        if (targetButton == null)
        {
            targetButton = GetComponent<Button>();
        }

        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
    }

    void Update()
    {
        if (targetButton == null || targetImage == null)
        {
            return;
        }

        // 押せるかどうか色変更
        targetImage.color =
            targetButton.interactable ? enableColor : disableColor;
    }
}