using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerTower : MonoBehaviour
{

    public float side = 0f;
    float ataqueCowldown = 3;
    bool atacou;
    public BoxCollider2D box;
    public EnemyTroop enemyTroop;
    float count;
    public int lifeMax = 100;
    public int life=100;

    public GameObject child;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = lifeMax;
        box = GetComponent<BoxCollider2D>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (atacou == true)
        {
            count += Time.deltaTime;
            if (count >= ataqueCowldown)
            {
                atacou = false;
                count = 0;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxInimigo"))
        {
            if (!atacou)
            {


                life -= 100;
                atacou = true;

                if (life <= 0)
                {
                    Debug.Log("vida");
                    //child.GetComponent<BoxCollider2D>().enabled = false;
                    enabled = false;
                    enemyTroop.atacarAnim = true;
                    box.enabled = false;
                }
            }


            Debug.Log(life);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTroop"))
        {
            if (!atacou)
            {


                life -= 100;
                atacou = true;

                
                collision.gameObject.GetComponent<EnemyTroop>().animator.SetBool("AtaqueEnemy", atacou);
                collision.gameObject.transform.LookAt(gameObject.transform.position);

                if (life <= 0)
                {
                   // morri = true;
                    //paiTorres.checagemtorre(morri);
                    //collision.enabled = false;
                    //enabled = false;
                    Destroy(this.gameObject);
                }
            }


            Debug.Log(life);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyTroop")
        {
            //if (!atacou)
            //{


            life -= 1;
            atacou = true;

            if (life <= 0)
            {
                // morri = true;
                //paiTorres.checagemtorre(morri);
                //collision.enabled = false;

                Destroy(this.gameObject);
            }
            //}


            Debug.Log(life);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTroop")
        {
            atacou = false;
        }
    }

}
    