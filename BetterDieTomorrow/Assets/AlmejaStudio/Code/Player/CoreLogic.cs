using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLogic : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    private int _currentHealth;
    [SerializeField] private float _coldDown;
    private bool _isOnColdDown;
    

    public void ReciveDamage(int damage)
    {
        if (!_isOnColdDown)
        {
            _currentHealth -= damage;
            Debug.Log("El Enemy Me golpea " + damage + ". Vida restante: " + _currentHealth);
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _isOnColdDown = true;
            _currentHealth -= 10; // Por ejemplo, resta 10 de salud al entrar en contacto con un enemigo
            StartCoroutine(StartCooldown());
        }
    }

    private void FixedUpdate()
    {
        if (_isOnColdDown)
        {
            _coldDown -= Time.fixedDeltaTime;
            if (_coldDown <= 0)
            {
                _isOnColdDown = false;
                _coldDown = 3f; // Reinicia el cooldown a 3 segundos
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(_coldDown);
        _isOnColdDown = false;
        _coldDown = 3f; // Reinicia el cooldown a 3 segundos
    }

}