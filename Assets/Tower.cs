using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackInterval = 1f;
    public AttackRangeDetector rangeDetector;

    private LineRenderer lineRenderer;
    private Transform currentTarget;
    private float attackTimer = 0f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            FindTarget();
        }

        if (currentTarget != null && attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            Attack(currentTarget);
        }
    }

    void FindTarget()
    {
        if (rangeDetector.enemiesInRange.Count == 0)
        {
            currentTarget = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        Transform closest = null;

        foreach (Transform enemy in rangeDetector.enemiesInRange)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;

            float dist = Vector2.Distance(transform.position, enemy.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy;
            }
        }

        currentTarget = closest;
    }

    protected virtual void Attack(Transform target)
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }

        StartCoroutine(ShowAttackLine(target.position));
    }

    IEnumerator ShowAttackLine(Vector3 targetPos)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPos);

        yield return new WaitForSeconds(0.1f); // 显示0.1秒后消失

        lineRenderer.enabled = false;
    }
}