using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour {
    [SerializeField]
    private GameObject MenuObj;
    private bool systemMenu;

    [SerializeField]
    private Toggle WF_toggle;
    // Use this for initialization
    void Start () {
        WF_toggle.isOn = Screen.fullScreen;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            systemMenu = !systemMenu;
            MenuFlag(systemMenu);
           // Debug.Log(systemMenu);
        }
    }

    public void MenuFlag(bool flag)
    {
        MenuObj.SetActive(flag);
        systemMenu = flag;
        if (flag) Time.timeScale = 0; else Time.timeScale = 1;
    }
    public void pressQuit()
    {
        Application.Quit();
    }
    private void FullWindowChange()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, WF_toggle.isOn);

    }

    public void BackToTitle()
    {
        Time.timeScale = 1;
        GameManager.instance.SwitchScene("title");
    }
}
