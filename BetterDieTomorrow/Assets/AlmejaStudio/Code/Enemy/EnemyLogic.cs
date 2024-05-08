using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private int _startingHealth;
    private int _currentHealth;
    private bool isDead;
    [SerializeField]private SOEnemy enemyData;

    void Start()
    {
        if (enemyData != null)
        {
            _startingHealth = enemyData.health;
            _currentHealth = _startingHealth;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un SOEnemy al enemigo.");
        }
    }

    public void ReciveDamage()
    {
        
    }
    
    public void LiveCheck()
    {
        
    }
    
    public void MoveTo()
    {
        
    }
    
    public void Attack()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponentInParent<Bullet>();
            
            if (bullet != null)
            {
                Debug.Log("Se encontró el componente Bullet en el padre del objeto colisionado.");
                GetDamage(bullet.Damage);
            }
            else
            {
                Debug.Log("No se encontró el componente Bullet en el padre del objeto colisionado.");
            }
        }

        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }
    
    public void Die()
    {
        isDead = true;
    }
    
}
