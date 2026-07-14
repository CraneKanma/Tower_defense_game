using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 50;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            DestroyTower();
        }
    }

    void DestroyTower()
    {
        Debug.Log(gameObject.name + " 被摧毁");
        Destroy(gameObject);
        // 后续可以在这里加：更新可放置格子状态、播放摧毁特效等
    }
}
