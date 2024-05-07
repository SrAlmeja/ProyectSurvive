using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet> _pool;

    [SerializeField] private GameObject defaultBullet;

    private void Awake()
    {
        Bullet aBullet1 = defaultBullet.GetComponent<Bullet>();
    }

    private void Start()
    {
        //_pool = new ObjectPool<Bullet>()
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate()
    }
}
