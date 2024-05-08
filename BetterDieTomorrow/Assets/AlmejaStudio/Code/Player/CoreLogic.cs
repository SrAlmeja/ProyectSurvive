using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLogic : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    [SerializeField] private int _currentHealth;
    
    [SerializeField] private float _reparingTime;
    private bool _isReparing = true;

    void Start()
    {
        _currentHealth = _initialHealth;
        //StartCoroutine(ReparingCoroutine()); > Reactivate for WinCondition
    }

    public void ReciveDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("El Core recibe " + damage + " puntos de daño. Vida restante: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("GameOver");
        //Destroy(gameObject);
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
        Debug.Log("¡Ganaste!");
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