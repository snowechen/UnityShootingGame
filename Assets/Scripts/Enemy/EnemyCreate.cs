using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreate : MonoBehaviour {

    public GameObject Enemy;
    public GameObject Enemy2;
    public GameObject Boss;

    public float CreateTime;
    float timer;

    public Text enemytext;

    PlayerHealth player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] normalenemy = GameObject.FindGameObjectsWithTag("Normal Enemy");
        GameObject[] Elite = GameObject.FindGameObjectsWithTag("Elite Enemy");
        GameObject[] Boss = GameObject.FindGameObjectsWithTag("Boss");

        timer += Time.deltaTime;
        
        if (timer >= CreateTime)
        {
            if (player.currentHealth <= 0) return;
            if(Random.Range(0, 500) < 5 + player.Level && Boss.Length < 1 + player.Level / 10)
                BossCreate();
            if(Random.Range(0, 100) < 9 + player.Level && Elite.Length < 10 + player.Level)
                enem2yCreate();
            if(normalenemy.Length < 20 + player.Level)
                enemyCreate();
            timer = 0;
        }

        CreateTime = 2 - (float)player.Level / 10 < 0.5f ? 0.5f : 2 - (float)player.Level / 10;
        enemytext.text = "Enemy: " + normalenemy.Length + "\nElite: " + Elite.Length + "\nBoss: " + Boss.Length;
    }

    void enemyCreate()
    {
        //timer = 0;
        Instantiate(Enemy, CreatePostion(Random.Range(0,4)), Quaternion.identity);
    }

    void enem2yCreate()
    {
       // timer = 0;
        Instantiate(Enemy2, CreatePostion(Random.Range(0, 4)), Quaternion.identity);
    }

    void BossCreate()
    {
        //timer = 0;
        Instantiate(Boss, CreatePostion(Random.Range(0, 4)), Quaternion.identity);
    }

    Vector3 CreatePostion(int num)
    {
        Vector3 pos = Vector3.zero;
        switch (num)
        {
            case 0:
                pos= new Vector3(0, 0, -30);
                break;
            case 1:
                pos = new Vector3(-30, 0, 0);
                break;
            case 2:
                pos = new Vector3(-5, 0, 24.5f);
                break;
            case 3:
                pos = new Vector3(30, 0, 0);
                break;
            default:
                break;
        }
        return pos;
    }
}
