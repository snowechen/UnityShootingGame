using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public float Exp;

    public AudioClip deathClip;
    Animator anim;
    AudioSource enemyAudio;
    bool isDead;
    bool playerDead;
    ParticleSystem hitparticles;
    CapsuleCollider capsulecollider;
    bool isSinking;

    public GameObject HpDisplay;
    public Image HpBar;

    public GameObject kusuri;

    PlayerHealth player;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
       
        hitparticles = GetComponentInChildren<ParticleSystem>();
        capsulecollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        Transform camera = Camera.main.transform;

        HpDisplay.transform.LookAt(camera);
        HpBar.fillAmount = (float)currentHealth / (float)startingHealth;
    }
    public void TakeDamage(int amount,Vector3 hitPoint)
    {
        if (isDead)
            return;
        enemyAudio.Play();
      
        currentHealth -= amount;

        hitparticles.transform.position = hitPoint;
        hitparticles.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        System.Array.ForEach(GetComponents<Collider>(),col=>col.enabled = false);
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

      if(Random.Range(0,100) < 8)
        {
            Instantiate(kusuri, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        }
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        player.ExpUpdate(Exp);
        Destroy(gameObject, 2f);
    }

   
}
