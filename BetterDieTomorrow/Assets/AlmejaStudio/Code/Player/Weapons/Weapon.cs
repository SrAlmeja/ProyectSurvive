using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<SOGun> Guns;
    private int currentGunIndex = 0;
    [SerializeField] private Transform spawnPoint;
    private bool isShooting = false;
    
    private void Start()
    {
        //Debug.Log("Current Gun Name: " + Guns[currentGunIndex].name);
    }
    
    public void ChangeWeapon()
    {
        // Incrementa el índice actual
        currentGunIndex++;

        // Si el índice es mayor o igual al tamaño de la lista, vuelve al principio
        if (currentGunIndex >= Guns.Count)
        {
            currentGunIndex = 0;
        }

        // Ejemplo de cómo acceder a los datos del arma actual después de cambiar
        Debug.Log("Current Gun Name: " + Guns[currentGunIndex].name);
    }
    
    private void BulletSpawn()
    {
        SOGun currentGun = Guns[currentGunIndex];
        Instantiate(currentGun.Bullet, spawnPoint.position, spawnPoint.rotation);
    }
    
    public void Shoot()
    {
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootBulletsWithInterval());
        }
    }

    private IEnumerator ShootBulletsWithInterval()
    {
        SOGun currentGun = Guns[currentGunIndex];
        float interval = 1f / currentGun.CadenceVelocity;

        while (isShooting)
        {
            BulletSpawn();
            yield return new WaitForSeconds(interval);
        }
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    
}
