using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class son_Bult_Motion : MonoBehaviour
{
    // public Transform mom_Bult;
    public float speed;
    public float timeLim;
    //private float r;//半徑
    //private float w;//角度
    //private float x;
    //private float y;
    private float time;

    public Vector3 mov;
    // Use this for initialization
    Vector3 startpos;
    public GameObject effect;
    void Start()
    {
        //Vector3 rota = transform.eulerAngles;
        //rota.x = 90;
        //this.transform.eulerAngles = rota;

        mov = new Vector3(Mathf.Sin(transform.eulerAngles.y), 0, Mathf.Cos(transform.eulerAngles.y));
        //GetComponent<Rigidbody>().AddForce(mov);
        startpos = transform.position;
        GetComponent<Rigidbody>().AddForce(mov * speed);
    }

    // Update is called once per frame
    void Update()
    {
       
            //time += Time.deltaTime;
            //if (time <= timeLim)
            //    GetComponent<Rigidbody>().AddForce(mov);
            //if (time > timeLim && time <= timeLim + 1)
            //{
            //    GetComponent<Rigidbody>().velocity = Vector3.zero;
            //}
            //if (time > timeLim + 1)
            //{
                
            //}
        
        if(Vector3.Distance(startpos,transform.position)>20)
            Destroy(gameObject);
        
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.SendMessage("TakeDamage",5);
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (col.CompareTag("Floor"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
