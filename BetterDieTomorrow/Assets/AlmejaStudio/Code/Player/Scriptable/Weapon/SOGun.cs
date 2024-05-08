using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun Config", menuName = "AlmejaStudio/Guns/Guns", order = 0)]
public class SOGun : ScriptableObject
{
    public string name;
    public GameObject Bullet;
    public float CadenceVelocity;
    public bool InfiniteBullets;
    public int Bullets;
}