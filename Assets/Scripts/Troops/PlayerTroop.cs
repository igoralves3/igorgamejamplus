using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!offensive)
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
        else
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = 100f;
            var tower = towers[0];
            if (transform.position.y < 0)
            {
                tower = towers[1];
            }

            /*
            foreach (var t in towers)
            {
                var delta = Vector3.Distance(transform.position, t.transform.position);
                if (delta < currentDelta)
                {
                    tower = t;
                    currentDelta = delta;
                }
            }*/
            agent.SetDestination(tower.transform.position);
        }
    }
}
