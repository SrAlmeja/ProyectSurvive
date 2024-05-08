using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun Config", menuName = "AlmejaStudio/Guns/Guns", order = 0)]
public class SOGun : ScriptableObject
{
    public string name;
    public GameObject Bullet;
    public Vector3 SpawnPoint;
    public int CadenceVelocity;
    public bool InfiniteBullets;
    public int Bullets;

    // Resto del script
}