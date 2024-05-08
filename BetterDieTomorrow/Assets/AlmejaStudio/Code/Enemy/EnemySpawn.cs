using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private int waves;
    [SerializeField] private float coldDownW;
    
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned.");
            return;
        }

        if (enemyPrefabs[0] == null)
        {
            Debug.LogWarning("Enemy prefab is null.");
            return;
        }

        Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
    }
}
