
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public GameObject enemyTroop;

    public static int coins = 0;

    public int framesCoins = 0;
    public int framesSpawn = 0;

    public GameObject[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");   
    }

    // Update is called once per frame
    void Update()
    {
        if (coins < 8)
        {
            framesCoins += 1;
            if (framesCoins >= 600)
            {
                framesCoins = 0;
                coins += 1;
            }
        }

        framesSpawn += 1;
        if (framesSpawn >= 600)
        {
            framesSpawn = 0;
            if (Random.Range(0,100) > 50)
            {
                OnOffense();
            }
            else
            {
                OnDefense();
            }
        }
    }

    private void OnOffense()
    {

        var index = Random.Range(0,spawnPoints.Length);

        var sp = spawnPoints[index];

        var worldPosition = new Vector3(sp.transform.position.x, sp.transform.position.y,-2);

        var it = Instantiate(enemyTroop, worldPosition, Quaternion.identity);
        it.GetComponent<EnemyTroop>().offensive = true;
    }

    private void OnDefense()
    {
        var index = Random.Range(0, spawnPoints.Length);

        var sp = spawnPoints[index];

        var worldPosition = new Vector3(sp.transform.position.x, sp.transform.position.y, -2);

        var it = Instantiate(enemyTroop, worldPosition, Quaternion.identity);
        it.GetComponent<EnemyTroop>().offensive = false;
    }
}
