using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] public float speed;
    [SerializeField] public int damage;

    public void OnObjectSpawn()
    {
        Move();
    }
    

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buildings"))
        {
            DestroyBullet();
        }
    }

    private void GiveDamage(GameObject target)
    {
        Debug.Log("Dealing damage to: " + target.name);
    }

    private void DestroyBullet()
    {
        Debug.Log("Destroying bullet");
        gameObject.SetActive(false);
    }
}