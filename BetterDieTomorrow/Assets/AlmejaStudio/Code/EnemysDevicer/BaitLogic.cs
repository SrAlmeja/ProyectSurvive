using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitLogic : MonoBehaviour
{
    [Header("TimeToRespawn")]
    [SerializeField] private float minTime, maxTime;

    private EnemysDevicer _enemysDevicer;

    private void Awake()
    {
        _enemysDevicer = FindObjectOfType<EnemysDevicer>();
        if (_enemysDevicer == null)
        {
            Debug.LogError("No se encontró un objeto con el componente EnemysDevicer en la escena.");
        }
    }

    
    private IEnumerator RespawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
            if (_enemysDevicer != null)
            {
                _enemysDevicer.SpawnFromPool();
            }
        }
    }
}