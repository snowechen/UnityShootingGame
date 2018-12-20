using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart_Motion : MonoBehaviour
{
    public float speed;
    public float timeLim;
    //Quaternion rota;
    public GameObject bult;
    //private int interval;
    private float time;
    //private int tag = 1;
    public enum type
    {
        normal = 0,
        half,
        left_half,
        right_half,
        special,
    };
    public type style = type.normal;
    private float sp_ro = 0;
    // Use this for initialization
    public GameObject ef;
    public float deadTime;
    void Start()
    {
        time = timeLim-0.1f;
        Vector3 mov = transform.rotation * Vector3.forward;
        GetComponent<Rigidbody>().AddForce(mov * speed * 10);
    }

    // Update is called once per frame
    void Update()
    {
        speed -= Time.deltaTime * 10;
        if (speed <= 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Boom();
            time += Time.deltaTime;
            deadTime -= Time.deltaTime;
        }
        if (deadTime <= 0) {
            Instantiate(ef, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
       
        if (style == type.special && time >= timeLim)
        {
            Quaternion rotation = new Quaternion();
            sp_ro += 4;
            rotation.eulerAngles = new Vector3(0, sp_ro * 0.2f, 0);
            Instantiate(bult, transform.position , rotation);
            time = 0;
        }
        
    }

    void Boom()
    {
        List<float> rotalist = new List<float>();
        switch ((int)style)
        {
            case (int)type.normal:
                for (int i = 0; i <= 25; i++)
                {
                    rotalist.Add(i * 0.25f);
                }
                break;
            case (int)type.half:
                for (int i = 0; i <= 15; i++)//30
                {
                    rotalist.Add(i * 0.2f);//0.4
                }
                break;
            case (int)type.left_half:
                for (int i = 8; i <= 23; i++)//30
                {
                    rotalist.Add(i * 0.2f);//0.4
                }
                break;
            case (int)type.right_half:
                for (int i = 15; i <= 30; i++)//30
                {
                    rotalist.Add(i * 0.2f);//0.4
                }
                break;
            default:
                break;
        }


        if (rotalist.Count > 0)
        {
            Quaternion rota = new Quaternion();

            rotalist.ForEach(r =>
            {
                rota.eulerAngles = new Vector3(0, r, 0);
                Instantiate(bult, transform.position + new Vector3(Mathf.Sin(r), 0, Mathf.Cos(r)), rota);
            });
            Instantiate(ef, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
