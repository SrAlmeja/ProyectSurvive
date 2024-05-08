using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private OSBullet bulletData;
    private GameObject bulletPrefab;
    private Rigidbody bulletRigidbody;
    private Collider bulletCollider;

    private void Awake()
    {
        bulletPrefab = Instantiate(bulletData.BulletPrefab, transform.position, transform.rotation, transform);
        bulletRigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
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

    public void GiveDamage()
    {
        int damage = bulletData.Damage; 
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}