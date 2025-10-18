using System;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTroop : MonoBehaviour
{

    public bool offensive = true;

    public NavMeshAgent agent;

    public GameObject currentTower;

    public int custoDeUso;
    public int QuantidadeDeUnidades;
    public int vida;
    public int poderDeAtaque;
    public int velocidadeDeMovimento;
    public int cadenciaDeAtaque;
    public int alcanceDeVisao;

    public GameObject espada;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        /*
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
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (offensive)
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = Mathf.Infinity;
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
            var currentDelta = Mathf.Infinity;
            var tower = towers[0];
            foreach (var t in towers)
            {
                 if (t != currentTower)
                    {
                        var delta = Vector3.Distance(transform.position, t.transform.position);
                        if (delta < currentDelta)
                        {
                            tower = t;
                            currentDelta = delta;
                        }
                    }
                
            }
            agent.SetDestination(tower.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTower"))
        {
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                //transform.LookAt(collision.gameObject.transform);
                espada.transform.position = transform.position - new Vector3(1, 0f, 0f);
            }
            else
            {
                espada.transform.position = transform.position + new Vector3(1, 0f, 0f);
            }
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //animator.SetBool("Atacar", true);

        }
    }
}
