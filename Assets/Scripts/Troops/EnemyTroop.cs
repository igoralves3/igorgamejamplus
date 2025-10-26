
using System.Collections.Specialized;

using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyTroop : MonoBehaviour
{
    public AudioSource audioData;
    public AudioClip clip;

    public bool offensive = true;
    public Animator animator;
    public bool atacarAnim;
    public NavMeshAgent agent;
    public float rotacaoOffset = 0f;
    public GameObject currentTower;

    public int custoDeUso;
    public int QuantidadeDeUnidades;
    public int vida;
    public int poderDeAtaque;
    public int velocidadeDeMovimento;
    public int cadenciaDeAtaque;
    public int alcanceDeVisao;
    public bool isDead;

    public GameObject espada;

    public int hp = 100;
    public bool goingToCenter = false;

    public int attack=10;

    private float ataqueCoolDown=3;
    private bool atacou = false;
    float count;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        atacarAnim = false;
    }

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator.SetBool("AtaqueEnemy", false);
        agent.updateUpAxis = false;

        if (offensive)
        {
            var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
            var currentDelta = Mathf.Infinity;

            if (towers.Length == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            var tower = towers[Random.Range(0, towers.Length)];//towerss[0];

            //var tower = towers[0];
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
            currentTower = tower;
        }
        else
        {
            var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
            var currentDelta = Mathf.Infinity;

            if (towers.Length == 0)
            {
                SceneManager.LoadScene("Victory");
            }

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
            currentTower = tower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (atacou == true)
        {
            count += Time.deltaTime;
            if (count >= ataqueCoolDown)
            {
                atacou = false;
                count = 0;
            }
        }

        // var e = currentTower.GetComponent<EnemyTower>();
        //var p = currentTower.GetComponent<PlayerTower>();
        if (currentTower == null)
        {
            if (offensive) {
                //animator.SetBool("AtaqueEnemy", false);
                //ChangeTower();
                
                GetTowerDestinationOfensive();
            }
            else
            {
                GetTowerDestinationDefensive();
            }
           // goingToCenter = true;
            //GoToCenter();
        }
        else
        {

            if (goingToCenter)
            {
                if (new Vector2(transform.position.x, transform.position.y) == new Vector2(0f, 0f))
                {
                    goingToCenter = false;
                    ChangeTower();
                }
            
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
       
        if (collision.gameObject.CompareTag("PlayerTower"))
        {
            
                Debug.Log("Player Tower trigger");
                if (transform.position.y > collision.gameObject.transform.position.y)
                {
                    //transform.LookAt(collision.gameObject.transform);
                    espada.transform.position = transform.position - new Vector3(0f, 1f, 0f);
                }
                else
                {
                    espada.transform.position = transform.position + new Vector3(0f, -1f, 0f);
                }
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("AtaqueEnemy", atacarAnim);
            //animator.SetBool("Atacar", true);
            SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1.0F);
            var c = collision.gameObject.GetComponent<PlayerTower>();
                if (c != null)
                {
                    c.life -= attack;


                    //currentTower = null;

                    //goingToCenter = true;
                    //GoToCenter();
                }
            
        }
        if (collision.gameObject.CompareTag("EnemyTower"))
        {
            if (!offensive)
            {
                if (hp < 100)
                {
                    hp++;
                }
                else
                {
                    //animator.SetBool("AtaqueEnemy", false);
                    ChangeTower();
                    GoToCenter();
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("PlayerTower"))
        {
            if (!atacou) {
                Debug.Log("Player Tower trigger stay");
                atacou = true;
                if (transform.position.y > collision.gameObject.transform.position.y)
                {
                    //transform.LookAt(collision.gameObject.transform);
                    espada.transform.position = transform.position - new Vector3(0f, 1f, 0f);
                }
                else
                {
                    espada.transform.position = transform.position + new Vector3(0f, 1f, 0f);
                }
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                animator.SetBool("AtaqueEnemy", true);
                //animator.SetBool("Atacar", true);
                SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1.0F);
                var c = collision.gameObject.GetComponent<PlayerTower>();
                if (c != null)
                {
                    //animator.SetBool("AtaqueEnemy", atacarAnim);
                    c.life -= attack;


                    //currentTower = null;

                    //goingToCenter = true;
                    //GoToCenter();
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTower"))
        {
            Debug.Log("Player Tower collision");
            if (transform.position.y > collision.gameObject.transform.position.y)
            {
                //transform.LookAt(collision.gameObject.transform);
                espada.transform.position = transform.position - new Vector3(0f, 1f, 0f);
            }
            else
            {
                espada.transform.position = transform.position + new Vector3(0f, -1f, 0f);
            }
            //var c = collision.gameObject.GetComponent<PlayerTower>();
            //if (c.life > 0)
            //{
            //    c.life -= 100;
            //}
            //if (c.life <= 0) {
            //    gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            //    animator.SetBool("AtaqueEnemy", false);
            //}
            //else
            //{
            //    gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //    animator.SetBool("AtaqueEnemy", true);
            //}

                
            //animator.SetBool("Atacar", true);
            //if (collision.gameObject.GetComponent<PlayerTower>() == null)
            //{
            //  currentTower = null;

            // goingToCenter = true;
            //GoToCenter();
            //}

        }
        if (collision.gameObject.CompareTag("EnemyTower"))
        {/*
            if (!offensive)
            {
                if (hp < 100)
                {
                    hp++;
                }
                else
                {
                    //animator.SetBool("AtaqueEnemy", false);
                    ChangeTower();
                    GoToCenter();
                }
            }
*/
            if (!offensive)
            {
                var c = collision.gameObject.GetComponent<EnemyTower>();
                if (c.life+10 < 100)
                {
                    c.life += 10;
                }
                else
                {
                    c.life = 100;
                    //ChangeTower();
                    //GoToCenter();
                    //GetTowerDestinationDefensive();
                }
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "PlayerTroop")
        {

            var c = collision.gameObject.GetComponent<PlayerTroop>();
            c.hp -= Random.Range(0, attack);

        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTower")
        {
            GoToCenter();
           // animator.SetBool("AtaqueEnemy", atacarAnim);

        }
    }

   

    void GetTowerDestinationDefensive()
    {
        var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
        var currentDelta = 0f;
        if (towers.Length == 0)
        {
            SceneManager.LoadScene("Victory");
        }

        var tower = towers[0];


        if (towers.Length > 1)
        {
            if (towers[0].transform.position.x > towers[1].transform.position.x)
            {
                var aux = towers[0];
                towers[0] = towers[1];
                towers[1] = aux;
            }

            if (transform.position.x > 0)
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

    void GetTowerDestinationOfensive()
    {
        var towers = GameObject.FindGameObjectsWithTag("PlayerTower");
        var currentDelta = 0f;

        if (towers.Length == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            var tower = towers[0];
            if (towers.Length > 1)
            {
                if (towers[0].transform.position.x > towers[1].transform.position.x)
                {
                    var aux = towers[0];
                    towers[0] = towers[1];
                    towers[1] = aux;
                }

                if (transform.position.x > 0)
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
    }

    void ChangeTower()
    {
        var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
        if (offensive)
        {
            
            towers = GameObject.FindGameObjectsWithTag("PlayerTower");
        }

            
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
       goingToCenter = true;
       agent.SetDestination(new Vector3(0f, 0f, 0f));
    }

    public void lidarComTrigger(scriptVisaoEnemy.TipoDeTrigger tipoDeTrigger,Collider2D playerCollider,GameObject target)
    {
            switch(tipoDeTrigger)
            {
            case scriptVisaoEnemy.TipoDeTrigger.Visao:
                animator.SetBool("AtaqueEnemy", true);
                SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1.0F);
                agent.Stop();

                Vector3 direcao = target.transform.position - espada.transform.position;

                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

                Quaternion rotacaoFinal = Quaternion.Euler(0f, 0f, -angulo + rotacaoOffset);
                espada.transform.rotation = rotacaoFinal;


               
               
              
                break;

        }
    }
   
}
