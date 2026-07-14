using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "TowerDefense/WaveData")]
public class WaveData : ScriptableObject
{
    public EnemySpawnInfo[] enemyGroups;
    public float delayBeforeWave = 3f;
}