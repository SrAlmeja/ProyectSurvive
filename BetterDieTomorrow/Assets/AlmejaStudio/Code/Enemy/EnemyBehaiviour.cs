using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaiviour : MonoBehaviour
{
    private GameObject player;
    private GameObject core;
    private GameObject _closserTarget;

    private EnemyLogic _eLogic;

    private void Awake()
    {
        _eLogic = GetComponent<EnemyLogic>(); 
        player = FindClosestWithTag("Player");
        core = FindClosestWithTag("Core");
    }

    private void Start()
    {
        StartCoroutine(CheckClosestTarget());
    }
    
    private void FixedUpdate()
    {
        if (_closserTarget != null)
        {
            if (!_eLogic.isDead)
            {
                _eLogic.MoveTo(_closserTarget.transform);    
            }
            else
            {
                _eLogic.StopEnemy();
            }
        }
    }

    private GameObject FindClosestWithTag(string tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject obj in objs)
        {
            Vector3 diff = obj.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = obj;
                distance = curDistance;
            }
        }

        return closest;
    }

    private GameObject CalculateClosestTarget()
    {
        if (player == null || core == null)
        {
            Debug.LogWarning("Player or core not found.");
            return null;
        }

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        float coreDistance = Vector3.Distance(transform.position, core.transform.position);

        if (playerDistance < coreDistance)
        {
            return player;
        }
        else
        {
            return core;
        }
    }
    
    private IEnumerator CheckClosestTarget()
    {
        while (true)
        {
            _closserTarget = CalculateClosestTarget();
            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
    }
}
