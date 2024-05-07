using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [System.Serializable]
    public class  Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        
    }

    [SerializeField] private List<Pool> pools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> poolDictionary;

    public static PoolObject Instance;
    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    private void Start()
    {
        AddPoolsToDictionary();
    }

    private void AddPoolsToDictionary()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with Tag: " + tag + " Doesn't exist");
            return null;
        }
        GameObject objectToSpawm = poolDictionary[tag].Dequeue();

        objectToSpawm.SetActive(true);
        objectToSpawm.transform.position = position;
        objectToSpawm.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawm.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawm);
        return objectToSpawm;
    }
}
