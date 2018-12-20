using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resolution : MonoBehaviour {
    
    int index = 0;
    private Resolution[] resolutionList;
    /// <summary>
    /// 获取分辨率信息
    /// </summary>
    public Resolution Get_resolution
    {
        get { return resolutionList[index]; }
    }

    [SerializeField]
    private Dropdown resoluDropdown;

    [SerializeField]
    private Toggle FWtg;//获取切换全屏的toggle控件
    public bool FullWindow { get; set; }//是否全屏的状态
    //[SerializeField]
    //private Text rosoluDisplay; //分辨率文字显示
    private void Start()
    {
        //清除分辨率下拉框的内容
        //resoluDropdown.ClearOptions();
        //初始化全屏开关
        FullWindow = Screen.fullScreen;
        FWtg.isOn = FullWindow;
        //规定分辨率数组长度
        resolutionList = new Resolution[Screen.resolutions.Length-6];
        //Debug.Log(Screen.currentResolution);
        //挑选有用的值给分辨率列表
        List<string> DropdownList = new List<string>();
        for (int i = 0; i < resolutionList.Length; i++)
        {
            resolutionList[i] = Screen.resolutions[i+6];//添加相应的分辨率到列表
            DropdownList.Add(resolutionList[i].width + " × " + resolutionList[i].height);
            // Debug.Log(resolutionList[i]);
        }
       // Debug.Log(resolutionList.Length);
        resoluDropdown.AddOptions(DropdownList);

        int _width = Screen.currentResolution.width;
        int _height = Screen.currentResolution.height;
        //设定当前分辨率的序号
        for (int i = 0; i < resolutionList.Length; i++)
        {
            if (resolutionList[i].width == _width && resolutionList[i].height == _height)
            {
                index = i;
                continue;
            }
        }
        //rosoluDisplay.text = _width + " × " + _height;
         resoluDropdown.value = index;
        
        //resolutionText.text = _width + " × " + _height;

    }

    public void FullWindowToggle(Toggle tg) {
        FullWindow = tg.isOn;
        //Debug.Log(FullWindow);
    }
   
    public void indexChange()
    {
        index = resoluDropdown.value;
       // index = index + sum >= resolutionList.Length ? 0 : index + sum < 0 ? resolutionList.Length - 1 : index + sum;
        var _width = resolutionList[index].width;
        var _height = resolutionList[index].height;
        //rosoluDisplay.text = _width + " × " + _height;
        Screen.SetResolution(_width, _height, FullWindow);
       // Debug.Log(index);
    }
}
