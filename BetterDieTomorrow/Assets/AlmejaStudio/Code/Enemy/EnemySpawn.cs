using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private int waves;
    
    [SerializeField] private float _coldDownWaveTime;
    private float _currentColdDownWT;
    private bool _isOnColdDown = false;
    
    private void Start()
    {
        StartCoroutine(StartWaves());
        _currentColdDownWT = _coldDownWaveTime;
    }

    public void Spawn(int count)
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

        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
        }
    }

    public void Waves()
    {
        Spawn(enemiesPerWave);
    }
    
    private IEnumerator StartWaves()
    {
        for (int waveIndex = 0; waveIndex < waves; waveIndex++)
        {
            yield return StartCoroutine(StartColdDownForNextWave());
            Waves();
        }
    }

    private IEnumerator StartColdDownForNextWave()
    {
        _isOnColdDown = true;
        while (_currentColdDownWT > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentColdDownWT--;
        }
        _isOnColdDown = false;
        _currentColdDownWT = _coldDownWaveTime;
    }
}
