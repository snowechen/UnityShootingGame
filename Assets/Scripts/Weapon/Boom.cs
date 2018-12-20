using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {

    [SerializeField]
    private float radius = 1.5f;
    [SerializeField]
    private float power = 600f;
    public int damage; 

    private void Start()
    {
        StartCoroutine(lifeTime());
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hits in colliders)
        {
            EnemyHealth enemy = hits.GetComponent<EnemyHealth>();
            if (hits.GetComponent<Rigidbody>() && enemy != null)
            {
                enemy.TakeDamage(damage,transform.position);
                hits.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius);
            }
        }
    }
    

    IEnumerator lifeTime()
    {
        while (GetComponent<ParticleSystem>().isPlaying)
        {
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
