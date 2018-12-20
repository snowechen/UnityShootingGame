using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField]
    private Fade fade;//遮罩
    [SerializeField]
    private Text loadText;//加载文字
    [SerializeField]
    private Image loadBar; //载入条
    [SerializeField]
    private String firstSceneName;
    [SerializeField]
    private Camera _loadingCamera;

    

    private bool isLodaingScene; // 是否处于加载Scene的过程中
    private void Awake()
    {
        if (instance) { Destroy(this.gameObject); }
        else
        {
            instance = this;
        }

    }  

   

    private void Start()
    {
        StartCoroutine(ChangeScene(firstSceneName));
    }

    /// <summary>
    /// 加载并切换到新的场景
    /// </summary>
    /// <param name="sceneName">场景名字</param>
    public void SwitchScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    /// <summary>
    /// 加载场景的协程
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator ChangeScene(string sceneName)
    {
        isLodaingScene = true;
        // 如果有别的场景在，就先淡出
        if (SceneManager.sceneCount >= 2)
        {
            fade.gameObject.SetActive(true);
            yield return fade.FadeOut();
        }

        // 先释放掉其他的场景
        while (SceneManager.sceneCount >= 2)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
        }

        // 然后开始加载新场景
        yield return LoadSceneAsync(sceneName);


        isLodaingScene = false;
        // 加载完成后淡入
        yield return fade.FadeIn();
        fade.gameObject.SetActive(false);
    }

    /// <summary>
    /// 异步加载一个场景
    /// </summary>
    /// <param name="sceneName">需要加载的场景名</param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        int displayProgress = 0;
        int toProgress = 0;
        setLoadingEffct(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;//暂时不激活加载的场景
        //显示加载进度，将进度平缓显示
        while (asyncOperation.progress < 0.9f)
        {
            toProgress = (int)asyncOperation.progress * 100;

            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;//将进度强行显示100,并平缓过度
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        //激活加载场景
        asyncOperation.allowSceneActivation = true;
        yield return new WaitForSeconds(1f);//等待1秒钟
        yield return asyncOperation.isDone;//等待场景完成返回值
        //将场景设为活动场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        setLoadingEffct(false);
    }
    /// <summary>
    /// 显示加载值
    /// </summary>
    /// <param name="progress"></param>
    void SetLoadingPercentage(int progress)
    {
        loadText.text =  progress + "%";
        loadBar.fillAmount = progress / 100f;
    }

    /// <summary>
    /// 开启与关闭加载文字和读条
    /// </summary>
    /// <param name="flag"></param>
    void setLoadingEffct(bool flag)
    {
        _loadingCamera.enabled = flag;
        _loadingCamera.GetComponent<AudioListener>().enabled = flag;
        loadText.enabled = flag;
        loadBar.enabled = flag;
    }


}
