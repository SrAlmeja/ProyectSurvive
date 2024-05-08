using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<SOGun> Guns;
    private int currentGunIndex = 0;

    private void Start()
    {
        // Ejemplo de cómo acceder a los datos del arma actual
        Debug.Log("Current Gun Name: " + Guns[currentGunIndex].name);
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
}