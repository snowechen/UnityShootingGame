using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    public bool isboss;
    float bosstimer;
    public GameObject[] buttle;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        bosstimer += Time.deltaTime;
        if(timer>= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth>0)
        {
            Attack();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
        if(isboss && bosstimer > 5 && Random.Range(0, 100) > 97)
        {
            BossAttack();
        }
    }

    void Attack()
    {
        timer = 0f;
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void BossAttack()
    {
        bosstimer = 0;
        Instantiate(buttle[Random.Range(0,buttle.Length)], transform.position + transform.forward * 2+new Vector3(0,0.9f,0), transform.rotation);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
        {
            playerInRange = true;
        }


    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
