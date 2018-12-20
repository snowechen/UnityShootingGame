using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BombUI : MonoBehaviour {
    public static BombUI instance;

    public float skillTime = 3;
    public Text skillNum;
    public Image skillBar;
    public int maxNum = 3;

    private int currNum = 0;
    public int CurrBombNum { get { return currNum; } set { currNum = value; } }
    private void Start()
    {
        instance = this;
        StartCoroutine(NumUpdate());
    }

    IEnumerator NumUpdate()
    {
        float currTime = skillTime;
        while (true)
        {
            if (currNum < maxNum)
            {
                currTime -= Time.deltaTime;
                skillBar.fillAmount = currTime / skillTime;
                if (currTime <= 0)
                {
                    currTime = skillTime;
                    currNum += 1;
                }
                skillNum.text = "手雷x <color=#67FEFF>" + currNum + "</color>";
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
