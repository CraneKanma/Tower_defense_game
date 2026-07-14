using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private float baseSpeed; // 记录原始速度，减速结束后恢复
    private Coroutine slowCoroutine; // 记录当前生效的减速协程，避免叠加冲突

    void Awake()
    {
        baseSpeed = moveSpeed;
    }

    public void SetPath(Transform[] path)
    {
        waypoints = path;
        currentWaypointIndex = 0;
        transform.position = waypoints[0].position;

        // 复用时记得重置速度，避免上一次的减速效果残留
        moveSpeed = baseSpeed;
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            slowCoroutine = null;
        }
    }

    void Update()
    {
        if (waypoints == null || currentWaypointIndex >= waypoints.Length)
            return;

        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                ReachEndPoint();
            }
        }
    }

    // 新增：应用减速效果
    public void ApplySlow(float slowMultiplier, float duration)
    {
        // 如果已经有减速效果在生效，先停止旧的，避免叠加导致速度异常
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }
        slowCoroutine = StartCoroutine(SlowRoutine(slowMultiplier, duration));
    }

    IEnumerator SlowRoutine(float multiplier, float duration)
    {
        moveSpeed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseSpeed; // 时间到，恢复原速
        slowCoroutine = null;
    }

    void ReachEndPoint()
    {
        GameManager.Instance.PlayerTakeDamage(1);
        WaveManager.Instance.OnEnemyRemoved();
        ObjectPool.Instance.ReturnToPool(gameObject);
    }
}