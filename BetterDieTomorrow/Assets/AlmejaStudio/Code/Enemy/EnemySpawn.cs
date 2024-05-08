using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private int waves;
    
    private float _coldDownWaveTime;
    private float _currentColdDownTime;
    
    private CoreLogic _coreLogic;
    
    private void Awake()
    {
        _coreLogic = FindObjectOfType<CoreLogic>();
        if (_coreLogic == null)
        {
            Debug.LogError("No se encontró un objeto con el componente CoreLogic en la escena.");
        }
    }
    
    private void Start()
    {
        _coldDownWaveTime = TimeCalculation();
        StartCoroutine(StartWaves());
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

    private float TimeCalculation()
    {
        if (_coreLogic == null)
        {
            Debug.LogError("No se encontró un objeto con el componente CoreLogic en la escena.");
            return 0f;
        }

        float timeToWin = _coreLogic.TimeToWin;
        float coldDownWavesTime = (timeToWin - 0.3f * timeToWin) / waves;
        return coldDownWavesTime;
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
        while (_currentColdDownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentColdDownTime--;
        }
        _currentColdDownTime = _coldDownWaveTime;
    }
}