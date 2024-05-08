using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    private int _startingHealth;
    private int _currentHealth;
    private int _damage;
    [HideInInspector] public bool isDead;
    private NavMeshAgent _agent;
    
    [SerializeField]private SOEnemy enemyData;
    private EnemyAnimationController _eAnimController;

    private PlayerController _playerController;
    private CoreLogic _coreLogic;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _eAnimController = GetComponent<EnemyAnimationController>();
        
        _playerController = FindObjectOfType<PlayerController>();
        _coreLogic = FindObjectOfType<CoreLogic>();

        if (_playerController == null)
        {
            Debug.LogError("No se encontró el componente PlayerController en la escena.");
        }

        if (_coreLogic == null)
        {
            Debug.LogError("No se encontró el componente CoreLogic en la escena.");
        }
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
        //Use it if there is time of damage animation
    }


    public void MoveTo(Transform target)
    {
        _agent.SetDestination(target.transform.position);
    }

    public void StopEnemy()
    {
        _agent.isStopped = true;
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
            _damage = enemyData.attackPower; ;
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
            isDead = true;
            Destroy();
        }
    }

    private void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            ScoreManager.IncrementScore(enemyData.droppedPoins);
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
        _eAnimController.Dead();
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (_playerController.isDead || _coreLogic.isDead)
        {
            StopEnemy();
        }
    }
}