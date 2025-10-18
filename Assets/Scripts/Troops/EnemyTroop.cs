using System;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTroop : MonoBehaviour
{

    public bool offensive = true;

    public NavMeshAgent agent;

    public GameObject currentTower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (offensive)
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = 100f;
            var tower = towers[0];
            foreach (var t in towers)
            {
                var delta = Vector3.Distance(transform.position, t.transform.position);
                if (delta < currentDelta)
                {
                    tower = t;
                    currentDelta = delta;
                }
            }
            agent.SetDestination(tower.transform.position);
        }
        else
        {
            var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
            var currentDelta = 100f;
            var tower = towers[0];
            foreach (var t in towers)
            {
                var delta = Vector3.Distance(transform.position, t.transform.position);
                if (delta < currentDelta)
                {
                    tower = t;
                    currentDelta = delta;
                }
            }
            agent.SetDestination(tower.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
