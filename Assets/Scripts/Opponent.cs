

using System.Collections.Specialized;
using System.Runtime.InteropServices;
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
                coins += 2;
            }
        }

        framesSpawn += 1;
        if (framesSpawn >= 1200)
        {
            framesSpawn = 0;
            var r = Random.Range(0, 100);
            if (r <= 30)
            {
                if (coins >= 1) {
                    coins -= 1;
                    OnOffense();
                }
            }
            else if (r > 30 && r <= 60)
            {
                if (coins >= 5) {
                    coins -= 5;
                    //OnOffense();
                    OnDefense();
                }
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
        var towers = GameObject.FindGameObjectsWithTag("EnemyTower");
        var currentDelta = Mathf.Infinity;

        var itc = it.GetComponent<EnemyTroop>();
        var tower = towers[0];
        if (worldPosition.x > 0)
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

        //var itc = it.GetComponent<EnemyTroop>();
        itc.offensive = false;
        itc.currentTower = tower;

        Debug.Log(itc.currentTower);
    }
}
