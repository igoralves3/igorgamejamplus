using UnityEngine;

public class VisionTroopScript : MonoBehaviour
{
    public enum TipoDeTrigger { Visao }
    public PlayerTroop playerTroop;
    public GameObject enemylutando;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTroop = GetComponentInParent<PlayerTroop>(); 
    }

    // Update is called once per frame
    void Update()
    {
        playerTroop.EnemyisDead(enemylutando);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyTroop"))
        {
            enemylutando = collision.gameObject;
            playerTroop.lidarComTrigger(TipoDeTrigger.Visao, collision, collision.gameObject);
        }

    }
    
}
