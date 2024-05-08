using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "AlmejaStudio/EnemyConfig", order = 0)]
public class SOEnemy : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int health;
    public float movementSpeed;
    public int attackPower;
    public int droppedPoins;
}