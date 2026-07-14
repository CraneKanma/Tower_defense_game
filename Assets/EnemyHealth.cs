using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    void OnEnable()
    {
        // 用OnEnable而不是Awake，因为敌人是对象池复用的，
        // 每次从池子里取出重新启用时都要重置血量
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
{
    EconomyManager.Instance.AddGold(5);
    WaveManager.Instance.OnEnemyRemoved();
    
    ObjectPool.Instance.ReturnToPool(gameObject); // 改成回收，而不是SetActive(false)
}
}
