using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Image healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0, 0, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    public AudioSource BGM;

    Text Bufftext;
    bool isplusbuff;
    float buffdisplaytime = 1f;

    Renderer Damage_color;
    Color p_color;
   
    private float NextLvExp;
    public float currExp;
    public int Level;
    public Text[] lvexp;
    public Image expslider;

    List<int> scores;

    public GameObject DieCanvas;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
        Bufftext = GetComponentInChildren<Text>();
        Bufftext.enabled = false;
        foreach(var color in transform.GetComponentsInChildren<Renderer>())
        {
            if (color.transform.name == "Player")
            {
                Damage_color = color;
            }
        }
        p_color = Damage_color.material.color;

        NextLvExp = (((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) % 10)) + (Level - 1) * 30;
        foreach(var t in lvexp)
        {
            if (t.name == "Lv") t.text = "Lv." + Level;
            if (t.name == "exp") t.text = currExp + "/" + NextLvExp;
        }
        expslider.fillAmount = currExp / NextLvExp;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
            p_color = new Color(1, 0.1f, 0.1f);
           
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            p_color = Color.Lerp(p_color, new Color(1, 1, 1), 0.05f);
        }
        damaged = false;
        
        if (isplusbuff)
        {
            Vector3 camera = Camera.main.transform.position;
            Bufftext.transform.GetComponentInParent<Canvas>().transform.LookAt(camera);
            buffdisplaytime -= Time.deltaTime;
            if (buffdisplaytime <= 0)
            {
                isplusbuff = false;
                Bufftext.enabled = false;
                buffdisplaytime = 1f;
            }
        }

        Damage_color.material.color = p_color;
        foreach (var t in lvexp)
        {
            if (t.name == "Lv") t.text = "Lv." + Level;
            if (t.name == "exp") t.text = currExp + "/" + NextLvExp;
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.fillAmount = (float)currentHealth / (float)startingHealth;
        playerAudio.Play();

        if(currentHealth<=0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }
    /// <summary>
    /// 玩家死亡
    /// </summary>
    IEnumerator Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
        BGM.Stop();
        yield return new WaitForSeconds(3);
        DieCanvas.SetActive(true);
    }

    /// <summary>
    /// 读取场景
    /// </summary>
    public void RestartLevel(InputField obj)
    {
        //打开排名文档，如果没有创建文档
        FileStream ifr = new FileStream(Application.dataPath + "/ScoreList.txt", FileMode.OpenOrCreate);
        ifr.Close();//关闭文档

        string[] txts = File.ReadAllLines(Application.dataPath + "/ScoreList.txt");
        List<string> list = new List<string>();
        System.Array.ForEach(txts, t => list.Add(t));//将文档每行添加到列表中
        string playertxt = obj.text +","+Level + "," + ScoreManager.score;
        list.Add(playertxt);//将本次玩家信息添加到文档
        //根据分数重新排序
        list.Sort((x, y) => int.Parse(y.Split(',')[2]) - int.Parse(x.Split(',')[2]));

        File.WriteAllLines(Application.dataPath + "/ScoreList.txt", list.ToArray());

        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider col)
    { 
        if (col.CompareTag("Item"))
        {
            currentHealth = currentHealth + 10 > startingHealth ? currentHealth = startingHealth : currentHealth + 10;
            Destroy(col.gameObject);
            healthSlider.fillAmount = (float)currentHealth / (float)startingHealth;
            Bufftext.enabled = true;
            Bufftext.text = "+10";
            isplusbuff = true;
        }
    }

    public void ExpUpdate(float exp)
    {
        currExp = currExp + exp >= NextLvExp ? LevelUp(currExp+exp) : currExp + exp;
        expslider.fillAmount = currExp / NextLvExp;

    }
    float LevelUp(float exp)
    {
        Level += 1;
        startingHealth += Level * 5;
        currExp =   exp - NextLvExp;
        NextLvExp = (((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((Level - 1), 3)) + 15) / 5 * ((Level - 1) * 2 + 20) % 10)) + (Level - 1) * 30;
        if (currExp >= NextLvExp) LevelUp(currExp);
        return currExp;
    }
}
