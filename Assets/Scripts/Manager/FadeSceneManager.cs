using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;


public enum Scenes
{
    TitleScene,
    MenuScene,
    GameScene,
    EndingScene,
}

public class FadeSceneManager : MonoBehaviour
{
    public static FadeSceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static FadeSceneManager instance;

    public CanvasGroup Fade_img;
    float fadeDuration = 1; //암전되는 시간
    public GameObject Loading;
    public TMP_Text Loading_text; //퍼센트 표시할 텍스트

    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 추가
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 제거*
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
        });
    }

    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true; //아래 레이캐스트 막기
        })
        .OnComplete(() => {
            StartCoroutine("LoadScene", sceneName); /// 씬 로드 코루틴 실행 ///
        });
        UIManager.GetInstance().ClearList(); // 씬이 바뀔때마다 UI매니저를 클리어해주겠다.
                                             // PrevScene = currentScene;
    }
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
