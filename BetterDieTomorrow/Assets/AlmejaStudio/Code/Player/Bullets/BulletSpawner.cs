using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private int bulletPerSecond;
    [SerializeField] private float speed;
    [SerializeField] private bool useObjectPool = false;

    private ObjectPool<Bullet> bulletPool;

    private float _lastSpawnTime;

    #endregion

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestoryObject, false, 100, 100_000);
    }

    private void FixedUpdate()
    {
        float delay = 1f / bulletPerSecond;
        if (_lastSpawnTime + delay < Time.time)
        {
            int bulletToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
            while (bulletToSpawnInFrame > 0)
            {
                if (!useObjectPool)
                {
                    Bullet instance = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                    instance.transform.SetParent(transform, true);

                    SpawnBullet(instance);
                }
                else
                {
                    bulletPool.Get();
                }

                bulletToSpawnInFrame--;
            }

            _lastSpawnTime = Time.time;
        }
    }

    private void OnGUI()
    {
        if (useObjectPool)
        {
            GUI.Label(new Rect(10,10,200,30), $"Total Pool Size: {bulletPool.CountAll}");
            GUI.Label(new Rect(10,10,200,30), $"Active Objects: {bulletPool.CountActive}");
        }
    }

    #region Pool Functions

    private Bullet CreatePooledObject()
    {
        Bullet instance = Instantiate(bulletPrefab, Vector3.zero, quaternion.identity);
        instance.disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void OnTakeFromPool(Bullet instance)
    {
        instance.gameObject.SetActive(true);
        SpawnBullet(instance);
        instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(Bullet instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void ReturnObjectToPool(Bullet instance)
    {
        bulletPool.Release(instance);
    }
    private void OnDestoryObject(Bullet instance)
    {
        Destroy(instance.gameObject);
    }
    private void SpawnBullet(Bullet instance)
    {
        #region SpawnLocationVariables

        float spawnLX = spawnArea.transform.position.x + spawnArea.center.x + UnityEngine.Random.Range(-1 * spawnArea.bounds.extents.x, spawnArea.bounds.extents.x);
        float spawnLY = spawnArea.transform.position.y + spawnArea.center.y + UnityEngine.Random.Range(-1 * spawnArea.bounds.extents.y, spawnArea.bounds.extents.y);
        float spawnLZ = spawnArea.transform.position.z + spawnArea.center.z + UnityEngine.Random.Range(-1 * spawnArea.bounds.extents.z, spawnArea.bounds.extents.z);

        #endregion
        
        Vector3 spawnLocation = new Vector3(spawnLX, spawnLY, spawnLZ);

        instance.transform.position = spawnLocation;

        instance.Shoot(spawnLocation, spawnArea.transform.right, speed);
    }

    #endregion

    
}
