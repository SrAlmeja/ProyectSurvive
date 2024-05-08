using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private int _startingHealth;
    private int _currentHealth;
    private int _damage;
    private bool isDead;
    
    [SerializeField]private SOEnemy enemyData;
    private EnemyAnimationController EAnimController;


    private void Awake()
    {
        EAnimController = GetComponent<EnemyAnimationController>();
    }

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
    
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public void Attack()
    {
        if (!isDead)
        {
            _damage = enemyData.attackPower;
            Debug.Log("Enemy is attacking!");
        }
        else
        {
            Debug.Log("Can't attack, enemy is dead.");
        }
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

        if (other.CompareTag("Player")|| other.CompareTag("Core"))
        {
            Attack();
            Destroy();
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
        Destroy();
    }

    private void Destroy()
    {
        EAnimController.Dead();
        Destroy(gameObject, 5f);
    }
    
}
