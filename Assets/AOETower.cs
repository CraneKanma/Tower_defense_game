using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOETower : Tower
{
    public float explosionRadius = 1.5f; // 冲击波影响半径
    public GameObject explosionEffectPrefab; // 冲击波视觉效果的Prefab

    protected override void Attack(Transform target)
    {
        // 在目标位置，检测周围范围内所有敌人
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(target.position, explosionRadius, LayerMask.GetMask("Default"));

        foreach (Collider2D col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }

        // 播放冲击波视觉效果
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, target.position, Quaternion.identity);
            Destroy(effect, 0.5f); // 特效播放完自动销毁
        }
    }
}