using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // 用字典管理多种不同Prefab各自的池子
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 从池子里取一个对象，如果没有可用的就新建
    public GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // 如果这个Prefab还没有对应的池子，先创建一个
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary.Add(prefab, new Queue<GameObject>());
        }

        Queue<GameObject> pool = poolDictionary[prefab];

        GameObject objToSpawn;

        // 池子里有现成的，直接复用
        if (pool.Count > 0)
        {
            objToSpawn = pool.Dequeue();
        }
        else
        {
            // 池子里没有可用的，新建一个
            objToSpawn = Instantiate(prefab);
        }

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);

        // 给对象加一个标记，记住它是从哪个Prefab池子里来的，方便回收时找对
        PooledObject pooledObj = objToSpawn.GetComponent<PooledObject>();
        if (pooledObj == null)
        {
            pooledObj = objToSpawn.AddComponent<PooledObject>();
        }
        pooledObj.sourcePrefab = prefab;

        return objToSpawn;
    }

    // 回收对象回池子
    public void ReturnToPool(GameObject obj)
    {
        PooledObject pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj == null || pooledObj.sourcePrefab == null)
        {
            // 找不到来源信息，直接销毁兜底，避免内存泄漏
            Destroy(obj);
            return;
        }

        obj.SetActive(false);

        if (!poolDictionary.ContainsKey(pooledObj.sourcePrefab))
        {
            poolDictionary.Add(pooledObj.sourcePrefab, new Queue<GameObject>());
        }

        poolDictionary[pooledObj.sourcePrefab].Enqueue(obj);
    }
}