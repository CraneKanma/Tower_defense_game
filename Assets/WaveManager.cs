using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public WaveData[] waves;      // 所有波次数据，按顺序放
    public Path pathManager;       // 拖入你的路径管理物体

    private int currentWaveIndex = 0;
    private int aliveEnemyCount = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveData wave = waves[currentWaveIndex];
             HUDManager.Instance.UpdateWave(currentWaveIndex + 1, waves.Length);

            Debug.Log("第 " + (currentWaveIndex + 1) + " 波即将开始");
            yield return new WaitForSeconds(wave.delayBeforeWave);

            yield return StartCoroutine(SpawnWave(wave));

            // 等这一波敌人全部死亡或到达终点，才进入下一波
            yield return new WaitUntil(() => aliveEnemyCount <= 0);

            currentWaveIndex++;
        }

        Debug.Log("所有波次已完成，游戏胜利！");
        GameManager.Instance.OnGameWin(); // 之后在GameManager里实现这个方法
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        foreach (EnemySpawnInfo group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                SpawnEnemy(group.enemyData);
                yield return new WaitForSeconds(group.spawnInterval);
            }
        }
    }

    void SpawnEnemy(EnemyData data)
{
    GameObject enemyObj = ObjectPool.Instance.SpawnFromPool(
        data.enemyPrefab, 
        pathManager.waypoints[0].position, 
        Quaternion.identity
    );
    
    EnemyMovement movement = enemyObj.GetComponent<EnemyMovement>();
    movement.SetPath(pathManager.waypoints); // 复用的敌人也要重新初始化路径状态

    aliveEnemyCount++;
}

    // 敌人死亡或到达终点时调用这个，通知WaveManager减少存活计数
    public void OnEnemyRemoved()
    {
        aliveEnemyCount--;
    }
}