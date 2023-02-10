using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Button NextBtn;
    ScenesManager sM;

    void Start()
    {
        sM = ScenesManager.GetInstance();
        NextBtn.onClick.AddListener(OnClickStart);
    }
    void OnClickStart()
    {
        SceneManager.LoadScene("WorkScene");
    }
}
