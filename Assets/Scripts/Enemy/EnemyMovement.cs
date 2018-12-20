using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav;
    public bool isElite;
    public float speedTimer;
    float skillCD=3;
    Animator anim;
    public PlayerHealth GetPlayer { get { return playerHealth; } }
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
	}


	void Update ()
	{
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
            skillCD -= Time.deltaTime;
            if (isElite && Vector3.Distance(transform.position, player.position) < 8 && skillCD <= 0)
            {
                speedTimer += Time.deltaTime;
                if (speedTimer < 2)
                {
                    nav.speed = 8;
                    anim.SetBool("Speed", true);
                }
                else { nav.speed = 3; anim.SetBool("Speed", false); skillCD = 3; }
            }
            else speedTimer = 0;
        }
        else
        {
            nav.enabled = false;
        }
    }





}
