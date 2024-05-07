using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private WeaponType weapon;
    [SerializeField] private Transform weaponParent;
    [SerializeField] private List<SOGun> Guns;

    //[SerializeField] private PlayerIK InverseKinemarics;
    [Header("Runtime Filled")] public SOGun ActiveGun;

    private void Start()
    {
        SOGun gun = Guns.Find(gun => gun.Type == weapon);
        if (gun == null)
        {
            Debug.LogError($"No SOGun Found for GunType: {gun}");
        }

        ActiveGun = gun;
        gun.Spawn(weaponParent, this);
    }
}
