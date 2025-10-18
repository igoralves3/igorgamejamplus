using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : MonoBehaviour
{
    public bool offensive = true;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject currentTower;

    public GameObject espada;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //foreach (var p in GameObject.FindGameObjectsWithTag("PlayerTroop"))
        //{
          //  if (p != this)
            //{
              //  Physics2D.IgnoreCollision(p.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            //}
        //}

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator.SetBool("Atacar", false);

        if (offensive)
        {
            var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
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
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = 0f;
            var tower =  towers[0];
            if (towers.Length > 1) {
                if (towers[0].transform.position.y > towers[1].transform.position.y)
                {
                    var aux = towers[0];
                    towers[0] = towers[1];
                    towers[1] = aux;
                }

                if (transform.position.y > 0)
                {
                    tower = towers[0];
                }
                else
                {
                    tower = towers[1];
                } }
                Debug.Log("tower " + tower.ToString());
           /*
            foreach (var t in towers)
            {
                var delta = Vector3.Distance(transform.position, t.transform.position);
                if (delta > currentDelta)
                {
                    tower = t;
                    currentDelta = delta;
                }
            }*/
            agent.SetDestination(tower.transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTower"))
        {
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                //transform.LookAt(collision.gameObject.transform);
                espada.transform.position =transform.position- new Vector3(1,0f,0f);
            }
            else
            {
                espada.transform.position = transform.position + new Vector3(1, 0f, 0f);
            }
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            animator.SetBool("Atacar", true);

        }
    }

    
}
