using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int DamagePerShot = 20;
    private int damage;
    public float TimeBetweenBullets = 0.15f;
    public float Range = 100;
    public GameObject Efect;

    float timer;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    PlayerHealth PH;

    [SerializeField]
    private GameObject Boom;
    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        PH = transform.parent.GetComponent<PlayerHealth>();
        damage = DamagePerShot + PH.Level * 5;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && timer >= TimeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) && BombUI.instance.CurrBombNum>0)
        {
            CreateBoom();
        }
        if (timer >= TimeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
        damage = DamagePerShot + PH.Level * 5;
    }

    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay,out shootHit, Range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, shootHit.point);
                Instantiate(Efect, shootHit.point, Quaternion.identity);
            }

            gunLine.SetPosition(1, shootHit.point);
        }
        else if(Physics.Raycast(shootRay,out shootHit, Range, LayerMask.GetMask("Floor")))
        {
            gunLine.SetPosition(1, shootHit.point);
            Instantiate(Efect, shootHit.point, Quaternion.identity);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * Range);
        }
    }

    void CreateBoom()
    {
        BombUI.instance.CurrBombNum -= 1;
        Vector3 pos = PlayerMovement.instance.GetMousePoint;
        pos.y = 2f;
        GameObject bomb = Instantiate(Boom, transform.position, Quaternion.identity) as GameObject;
        bomb.GetComponent<Bomb>().damage = DamagePerShot*2 + PH.Level * 7;
        bomb.GetComponent<Rigidbody>().AddForce(pos*50 );
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
