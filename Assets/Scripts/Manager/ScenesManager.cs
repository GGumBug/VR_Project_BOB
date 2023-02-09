using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scenes
{
    TitleScene,
    MenuScene,
    GameScene,
    EndingScene,
}
public class ScenesManager : MonoBehaviour
{
    #region SingletoneMake
    public static ScenesManager instance = null;
    public static ScenesManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@ScenesManager");
            instance = go.AddComponent<ScenesManager>();

            DontDestroyOnLoad(go);
        }
        return instance;
    }
    #endregion
    #region Scene Control
    public Scenes currentScene;
    public CanvasGroup Fade_img;
    float fadeDuration = 2;

    public void ChangeScene(Scenes scenes)
    {
        Fade_img.DOFade(1, fadeDuration)
    .OnStart(() => {
        Fade_img.blocksRaycasts = true; //아래 레이캐스트 막기
    })
    .OnComplete(() => {
        StartCoroutine("LoadScene", scenes);
        //로딩화면 띄우며, 씬 로드 시작
    });
        UIManager.GetInstance().ClearList(); // 씬이 바뀔때마다 UI매니저를 클리어해주겠다.
                                             // PrevScene = currentScene;
        currentScene = scenes;
        SceneManager.LoadScene(scenes.ToString());

    }
    #endregion

    #region FadeOut
    public GameObject Loading;
    public Text Loading_text;

    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true); //로딩 화면을 띄움

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; //퍼센트 딜레이용

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; //씬 전환 준비 완료
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            Loading_text.text = percentage.ToString("0") + "%"; //로딩 퍼센트 표기
        }
    }
}
#endregion