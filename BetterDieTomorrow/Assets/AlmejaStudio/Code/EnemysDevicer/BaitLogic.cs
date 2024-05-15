using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitLogic : MonoBehaviour
{
    [Header("TimeToRespawn")]
    [SerializeField] private float minTime, maxTime;

    private EnemysDevicer _enemysDevicer;

    [SerializeField] private float constantTimeToRespawn;
    private float _respawnTimer;
    private bool _isRespawning;

    private void Awake()
    {
        _enemysDevicer = FindObjectOfType<EnemysDevicer>();
        if (_enemysDevicer == null)
        {
            Debug.LogError("No se encontró un objeto con el componente EnemysDevicer en la escena.");
        }
    }

    private void Start()
    {
        StartCoroutine(RespawnRoutine());
    }

    private Vector3 GetWorldCenter()
    {
        return new Vector3(0, transform.position.y, 0);
    }

    private float WorldCenterDistance()
    {
        Vector3 worldCenter = GetWorldCenter();
        return Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), worldCenter);
    }


    private float SetTimeToRespawn()
    {
        float distanceToCenter = WorldCenterDistance();
        float k = constantTimeToRespawn;
        float timeFactor = 1.0f - Mathf.Exp(-k * distanceToCenter);
        return minTime + (maxTime - minTime) * timeFactor;
    }

    private void BackToPool()
    {
        _enemysDevicer.ReturnToPool( gameObject);
    }
    
    private IEnumerator RespawnRoutine()
    {
        _isRespawning = true;
        float timer = SetTimeToRespawn();
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }

        _isRespawning = false;
    }
    
    
}