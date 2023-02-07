using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void ChangeScene(Scenes scene)
    {
        UIManager.GetInstance().ClearList(); // 씬이 바뀔때마다 UI매니저를 클리어해주겠다.
                                             // PrevScene = currentScene;
        currentScene = scene;
        SceneManager.LoadScene(scene.ToString());

    }
    #endregion
}
