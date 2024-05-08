using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Config", menuName = "AlmejaStudio/Guns/Bullet Configuration", order = 1)]
public class OSBullet : ScriptableObject
{
    public string Name;
    public float Speed;
    public int Damage;
    public GameObject BulletPrefab;
    public SOTrailConfig TrailConfig;
}