using UnityEngine;


public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int coin = 1000;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCoin(int amount)
    {
        coin += amount;
    }

    public bool SpendCoin(int amount)
    {
        if (coin < amount)
        {
            Debug.Log("コイン不足");
            return false;
        }

        coin -= amount;
        SaveManager.Instance.Save();
        Debug.Log("残りコイン: " + coin);
        return true;
    }

    public bool HasEnough(int amount)
    {
        return coin >= amount;
    }

    public int GetCoin()
    {
        return coin;
    }

    public void SetCoin(int coins)
    {
        coin = coins;

    }

}