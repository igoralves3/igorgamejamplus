using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaiTorres : MonoBehaviour

{
    public bool torreA1;
    public bool torreA2;
    public bool torreE1;
    public bool torreE2;
    [SerializeField]public GameObject[] Towers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Towers = gameObject.GetComponent<GameObject[]>();
    }

    // Update is called once per frame
    void Update()
    {
        var torresPlayer = GameObject.FindGameObjectsWithTag("PlayerTower");
        var torresEnemy = GameObject.FindGameObjectsWithTag("EnemyTower");

        bool pw = true ;
        bool pl = true;

        if (torresPlayer.Length == 0)
        {
            pl = true;
        }
        else
        {
            pl = false;
        }
        if (torresEnemy.Length == 0)
        {
            pw = true;
        }
        else
        {
            pw = false;
        }

            foreach (var t in torresPlayer)
            {

                if (t != null && t.GetComponent<PlayerTower>() != null && t.GetComponent<PlayerTower>().life > 0)
                {
                    pw = false;
                    break;
                }
            }
        if (pw)
        {
            SceneManager.LoadScene("Victory");
        }
        foreach (var t in torresEnemy)
        {
           
            if (t != null && t.GetComponent<EnemyTower>() != null && t.GetComponent<EnemyTower>().life > 0)
            {
                pl = false;
                break;
            }
        }
        if (pl)
        {
            SceneManager.LoadScene("GameOver");
        }
        /*
        if (torresEnemy.Length == 0)
        {
            SceneManager.LoadScene("Victory");
        }
        else if (torresPlayer.Length == 0)
        {
            SceneManager.LoadScene("GameOver");
        }*/
    }

    public void  checagemtorre(bool f)
    {
        for  (int i = 0; i < Towers.Length; i++)

            {
            if (i < 2)
            {
                var c = Towers[i].GetComponent<PlayerTower>();
                
            }
            else
            {
                var c = Towers[i].GetComponent<EnemyTower>();
                c.morri = f;
            }
            //if (Towers[i].morri)
         
        }
    }
}
