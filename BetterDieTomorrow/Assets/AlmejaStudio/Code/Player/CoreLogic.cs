using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLogic : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    [SerializeField] private int _currentHealth;
    
    [SerializeField] private float _reparingTime;
    private bool _isReparing = true;
    private bool isDead = false;

    private WinLoseConditionalController _winLose;

    public float TimeToWin
    {
        get
        {
            return _reparingTime;
        }
        set
        {
            _reparingTime = value;
        }
    }

    private void Awake()
    {
        _winLose = FindObjectOfType<WinLoseConditionalController>();
        if (_winLose == null)
        {
            Debug.LogError("No se encontró el componente WinLoseConditionalController en la escena.");
        }
    }

    void Start()
    {
        _currentHealth = _initialHealth;
        StartCoroutine(ReparingCoroutine());
    }

    public void ReciveDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("El Core recibe " + damage + " puntos de daño. Vida restante: " + _currentHealth);

        if (!isDead)
        {
            if (_currentHealth <= 0)
            {
                isDead = true;
                Die();
            }    
        }
        
    }

    private void Die()
    {
        Time.timeScale = 0f;
        _winLose.ShowDefeat();
    }

    private IEnumerator ReparingCoroutine()
    {
        _isReparing = true;
        float timer = _reparingTime;
        while (timer > 0)
        {
            Debug.Log("Tiempo restante: " + timer.ToString("0"));
            yield return new WaitForSeconds(1f);
            timer--;
        }
        _isReparing = false;
    }

    private void Win()
    {
        _winLose.ShowVictory();
        Time.timeScale = 0f;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLogic enemy = other.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                ReciveDamage(enemy.Damage);
            }
        }

        if (other.CompareTag("Player"))
        {
            if (_isReparing)
            {
                Debug.Log("Maquina Espacio Tiempo Quantica Magica sigue sibre cargada, " +
                          "tiempo estimado de reparacion" + _reparingTime);
            }
            else
            {
                Win();
            }
        }
    }
}