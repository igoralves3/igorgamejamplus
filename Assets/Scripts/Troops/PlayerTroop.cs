using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : MonoBehaviour
{
    public bool offensive = true;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject currentTower;
    public Transform torre;
    float rotacaoOffset = 0f;

    public GameObject espada;

    public int hp = 100;
    public bool goingToCenter = false;
    
    public int attack = 10;

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
            var towerss = GameObject.FindGameObjectsWithTag("EnemyTower");
            var currentDelta = Mathf.Infinity;
            var tower = towerss[0];
            foreach (var t in towerss)
            {
                var delta = Vector3.Distance(transform.position, t.transform.position);
                if (delta < currentDelta)
                {
                    tower = t;
                    currentDelta = delta;

                }
            }
            agent.SetDestination(tower.transform.position);
            torre = tower.transform;
        }
        else
        {
            GetTowerDestinationDefensive();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (currentTower == null)
        {
           
            //   ChangeTower();
        }

        if (goingToCenter)
        {
            if (new Vector2(transform.position.x, transform.position.y) == new Vector2(0f, 0f))
            {
                goingToCenter = false;
                ChangeTower();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("EnemyTower"))
        {
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                Vector3 direcao = torre.position - espada.transform.position;

                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

                Quaternion rotacaoFinal = Quaternion.Euler(0f, 0f, -angulo + rotacaoOffset);
                espada.transform.rotation = rotacaoFinal;

                espada.transform.position = transform.position - new Vector3(1, 0f, 0f);

            }
            else
            {
                Vector3 direcao = torre.position - espada.transform.position;

                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

                Quaternion rotacaoFinal = Quaternion.Euler(0f, 0f, angulo + rotacaoOffset);
                espada.transform.rotation = rotacaoFinal;
                espada.transform.position = transform.position + new Vector3(-1, 0f, 0f);

            }
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            animator.SetBool("Atacar", true);

        }
        if (collision.gameObject.CompareTag("PlayerTower"))
        {
            if (!offensive)
            {
                if (hp < 100)
                {
                    hp++;
                }
                else
                {
                    //ChangeTower();
                    GoToCenter();
                }
            }

        }
        if (collision.gameObject.CompareTag("BoxInimigo"))

        {
            hp -= 10;

            if (hp <= 0)
            {
                Destroy(this.gameObject);

            }
        }
    }

        public void GetTowerDestinationDefensive()
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = 0f;
            var tower = towers[0];
            if (towers.Length > 1)
            {
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
                }
            }

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

            currentTower = tower;
        }

        void ChangeTower()
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            foreach (var t in towers)
            {
                if (t != currentTower)
                {
                    currentTower = t;
                    break;
                }
            }
            agent.SetDestination(currentTower.transform.position);
        }

        void GoToCenter()
        {
            agent.SetDestination(new Vector3(0f, 0f, 0f));
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "EnemyTroop")
            {

                //var c = collision.gameObject.GetComponent<EnemyTroop>();
                //c.hp -= Random.Range(0,attack); 

            }
        }
        public void lidarComTrigger(VisionTroopScript.TipoDeTrigger tipoDeTrigger, Collider2D playerCollider, GameObject target)
        {
            switch (tipoDeTrigger)
            {

                case VisionTroopScript.TipoDeTrigger.Visao:
                    animator.SetBool("Atacar", true);
                    agent.Stop();
                    Vector3 direcao = target.transform.position - espada.transform.position;

                    float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

                    Quaternion rotacaoFinal = Quaternion.Euler(0f, 0f, -angulo + rotacaoOffset);
                    espada.transform.rotation = rotacaoFinal;

                    break;
            }
        }
        
    

    public void EnemyisDead(bool dead)
    {

    }
}
