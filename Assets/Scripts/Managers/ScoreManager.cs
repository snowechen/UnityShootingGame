using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;


    Text text;
    public Text dieText;
   
    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
        dieText.text = "目前得分：<size=14><color=#00FFEA>" + score + "</color></size>";
    }
}
