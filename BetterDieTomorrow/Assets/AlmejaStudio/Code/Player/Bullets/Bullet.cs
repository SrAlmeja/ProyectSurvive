using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private OSBullet bulletData;
    private GameObject bulletPrefab;
    private Rigidbody bulletRigidbody;
    private Collider bulletCollider;
    private int _damage;

    private void Awake()
    {
        bulletPrefab = Instantiate(bulletData.BulletPrefab, transform.position, transform.rotation, transform);
        bulletRigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        _damage = bulletData.Damage;
        TrailCreator();
        Move();
    }

    private void TrailCreator()
    {
        TrailRenderer trail = bulletPrefab.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.emitting = true;
        }
    }

    private void Move()
    {
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = transform.forward * bulletData.Speed;
        }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}