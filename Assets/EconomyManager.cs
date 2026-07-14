using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    public int currentGold = 100; // 初始金币，你可以自己调数值

    void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        HUDManager.Instance.UpdateGold(currentGold);
    }
    public void AddGold(int amount)
    {
        currentGold += amount;
        HUDManager.Instance.UpdateGold(currentGold);
        Debug.Log("获得金币：" + amount + "，当前金币：" + currentGold);

        // 之后可以在这里加：更新UI显示金币数
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            HUDManager.Instance.UpdateGold(currentGold); 
            Debug.Log("花费金币：" + amount + "，当前金币：" + currentGold);
            return true; // 扣款成功
        }
        else
        {
            Debug.Log("金币不足");
            return false; // 扣款失败（钱不够）
        }
    }
}
