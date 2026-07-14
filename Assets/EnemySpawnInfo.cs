using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public EnemyData enemyData;
    public int count;          // 这波里这种敌人生成几个
    public float spawnInterval = 0.5f; // 这种敌人之间生成的间隔
}


