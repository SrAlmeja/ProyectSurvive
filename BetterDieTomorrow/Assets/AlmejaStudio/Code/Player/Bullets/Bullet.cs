using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int damage;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GiveDamage(collision.gameObject);
        DestroyBullet();
    }

    private void GiveDamage(GameObject target)
    {
        Debug.Log("Dealing damage to: " + target.name);
    }

    private void DestroyBullet()
    {
        Debug.Log("Destroying bullet");
        transform.gameObject.SetActive(false);
    }
}

