using UnityEngine;

public class scriptVisaoEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum TipoDeTrigger{Visao }
    public GameObject enemyLutando;
    public EnemyTroop enemyTroop;
    
    void Start()
    {
        enemyTroop = GetComponentInParent<EnemyTroop>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(  collision.gameObject.CompareTag("PlayerTroop"))
        {
           
            enemyTroop.lidarComTrigger(TipoDeTrigger.Visao, collision, collision.gameObject);
            
        }

    }
    
}
