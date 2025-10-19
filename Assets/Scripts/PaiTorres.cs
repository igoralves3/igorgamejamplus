using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

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
