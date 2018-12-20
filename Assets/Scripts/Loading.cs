using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float currentAmount;

    public Image Loadbar;
    public Text Loadtext;

    //public GameObject player;
    bool isgamestart;
    float ingameTime = 2;
    void Awake () {
        isgamestart = false;
        //player.transform.position = new Vector3(-79, 10.6f, -92);
        currentAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentAmount < 100)
        {
            //player.GetComponent<Animator>().SetBool("IsWalking", true);
            currentAmount += speed * Time.deltaTime;
            Loadtext.text = ((int)currentAmount).ToString() + "%";
            //Vector3 pos = player.transform.position;
             //pos.x = -79 + 16.5f * (currentAmount / 100);

             //player.transform.position = pos;
        }
        if ((int)currentAmount == 100 && !isgamestart) {
            //player.GetComponent<Animator>().SetTrigger("Die");
            isgamestart = true;
        }
        Loadbar.fillAmount = currentAmount / 100;

        if(isgamestart)
        {
            ingameTime -= Time.deltaTime;
            if (ingameTime <= 0) SceneManager.LoadScene("main");
        }
    }
}
