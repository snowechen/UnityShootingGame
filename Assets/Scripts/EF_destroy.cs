using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_destroy : MonoBehaviour {

    public float DestroyTime;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, DestroyTime);
	}
	
	
}
