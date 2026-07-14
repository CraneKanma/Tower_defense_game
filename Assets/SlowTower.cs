using UnityEngine;

public class SlowTower : Tower
{
    public float slowMultiplier = 0.5f;
    public float slowDuration = 2f;
    public GameObject slowEffectPrefab;   // 贴在敌人身上的减速光环
    public GameObject iceBallPrefab;       // 飞行的冰球本体

    protected override void Attack(Transform target)
    {
        if (iceBallPrefab == null)
        {
            Debug.LogWarning("SlowTower没有配置IceBall Prefab");
            return;
        }

        GameObject iceBallObj = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);
        IceBall iceBall = iceBallObj.GetComponent<IceBall>();
        iceBall.Init(target, attackDamage, slowMultiplier, slowDuration, slowEffectPrefab);
    }
}