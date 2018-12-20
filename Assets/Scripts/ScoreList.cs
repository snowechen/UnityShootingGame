using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour {

    public Text Num;
    public Text level;
    public Text scorelist;
    public Text Name;
	void Awake () {
        
        FileStream ifr = new FileStream(Application.dataPath + "/ScoreList.txt", FileMode.OpenOrCreate);
        ifr.Close();
        string[] txts = File.ReadAllLines(Application.dataPath + "/ScoreList.txt");

        Num.text = "排名\n";
        level.text = "最高等级\n";
        scorelist.text = "分数\n";
        Name.text = "姓名\n";
        
        int num = 0;
        foreach (var t in txts)
        {
            if (num > 9) return;
            num++;
            string[] str = t.Split(',');

            Num.text += "<size=14>" + num+ "</size>\n";
            Name.text += str[0] + "\n";
            level.text += str[1]+"\n";
            scorelist.text += str[2]+"\n";
        }
        

    }
	
	
}
