using UnityEngine;
using System.Collections.Generic;

public class AttackRangeDetector : MonoBehaviour
{
    public List<Transform> enemiesInRange = new List<Transform>();

    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("检测到物体进入范围：" + other.gameObject.name + "，Tag: " + other.tag);
    
    if (other.CompareTag("Enemy"))
    {
        enemiesInRange.Add(other.transform);
        Debug.Log("成功加入攻击列表，当前范围内敌人数：" + enemiesInRange.Count);
    }
}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}