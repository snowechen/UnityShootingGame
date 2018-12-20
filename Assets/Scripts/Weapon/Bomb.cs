using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public int damage;
    public GameObject boom;
    public float boomtime;
	// Use this for initialization
	void Start () {
        StartCoroutine(CreateBoom());
	}
	
    IEnumerator CreateBoom()
    {
        yield return new WaitForSeconds(boomtime);

        GameObject boomEF = Instantiate(boom, transform.position, boom.transform.rotation) as GameObject;
        boomEF.GetComponent<Boom>().damage = damage;
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
