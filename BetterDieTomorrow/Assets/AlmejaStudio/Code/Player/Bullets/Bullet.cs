using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody rigidBodyBullet;

    public delegate void OnDisableCallback(Bullet Instance);
    public OnDisableCallback disable;
    
    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    public void Shoot(Vector3  Position, Vector3 Direction, float Speed)
    {
        rigidBodyBullet.velocity = Vector3.zero;
        transform.position = Position;
        transform.forward = Direction;
        
        rigidBodyBullet.AddForce(Direction * Speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buildings"))
        {
            rigidBodyBullet.velocity = Vector3.zero;
            GiveDamage(other.gameObject);
            DestroyBullet();
        }
    }

    private void GiveDamage(GameObject target)
    {
        Debug.Log("Dealing "+ damage + " of damage to: " + target.name);
    }

    private void DestroyBullet()
    {
        Debug.Log("Destroying bullet");
        disable?.Invoke(this);
    }
}