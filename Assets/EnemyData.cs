using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "TowerDefense/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public int reward = 5; // 打死后奖励金币
}
