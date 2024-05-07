using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private PoolObject _poolObject;

    private void Awake()
    {
        _poolObject = PoolObject.Instance;
    }

    private void FixedUpdate()
    {
        GameObject bullet = _poolObject.SpawnFromPool("DefaultBullet", transform.position, Quaternion.identity);
        bullet.transform.parent = transform;
    }
}
