using Unity.VisualScripting;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public float side = 0f;
    float ataqueCowldown = 3;
    bool atacou;
    float count;
    public int lifeMax = 100;
    public int life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = lifeMax;
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

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoxAtaque"))
        {
            if (!atacou)
            {
                

                life -= 100;
                atacou = true;

                if (life <= 0)
                {
                    Destroy(this.gameObject);
                }
            }


            Debug.Log(life);
        }
    }
}
