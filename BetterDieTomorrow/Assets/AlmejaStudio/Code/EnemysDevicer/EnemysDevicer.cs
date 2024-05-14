using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnemysDevicer : MonoBehaviour
{
    [Header("Teddys")]
    [SerializeField] private GameObject devicerPrefab;
    [SerializeField] private GameObject parentRef;
    [SerializeField] private int poolSize;

    [Header("Spawn Position")]
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    private float _xPosition;
    private float _yPosition;
    private Vector3 _spawnPosition;
    
    
    private void Start()
    {
        LeanPool.Despawn(devicerPrefab);

        for (int i = 0; i < poolSize - 1; i++)
        {
            SpawnFromPool();
        }
    }

    private void SetARange()
    {
        _xPosition = UnityEngine.Random.Range(xMin, xMax);
        _yPosition = UnityEngine.Random.Range(yMin, yMax);
        _spawnPosition = new Vector3(_xPosition, 0.5f, _yPosition);
    }

    public void SpawnFromPool()
    {
        SetARange();
        LeanPool.Spawn(devicerPrefab, _spawnPosition, Quaternion.identity, parentRef.transform);
        devicerPrefab.SetActive(false);
    }
}
