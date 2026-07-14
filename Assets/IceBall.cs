using UnityEngine;

public class IceBall : MonoBehaviour
{
    public float speed = 8f;
    
    private Transform target;
    private int damage;
    private float slowMultiplier;
    private float slowDuration;
    private GameObject slowEffectPrefab;

    // 由SlowTower调用，初始化这颗冰球要打的目标和参数
    public void Init(Transform target, int damage, float slowMultiplier, float slowDuration, GameObject slowEffectPrefab)
    {
        this.target = target;
        this.damage = damage;
        this.slowMultiplier = slowMultiplier;
        this.slowDuration = slowDuration;
        this.slowEffectPrefab = slowEffectPrefab;
    }

    void Update()
    {
        // 目标已经死亡/消失，冰球自己也消失，避免打空气
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        // 朝目标飞行
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 冰球飞行方向朝向目标（视觉上更自然）
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 命中判定
        if (Vector3.Distance(transform.position, target.position) < 0.15f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        EnemyMovement enemyMovement = target.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.ApplySlow(slowMultiplier, slowDuration);
        }

        // 生成减速视觉特效，贴在敌人身上
        if (slowEffectPrefab != null)
        {
            GameObject effect = Instantiate(slowEffectPrefab, target.position, Quaternion.identity);
            effect.transform.SetParent(target);
            Destroy(effect, slowDuration);
        }

        Destroy(gameObject); // 冰球命中后自己销毁
    }
}