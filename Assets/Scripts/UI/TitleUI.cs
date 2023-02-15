using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleUI : MonoBehaviour
{
    public Button NextBtn;
    ScenesManager sM;
    public TMP_InputField inputField;
    string playerName;
    public GameObject inputFieldPanel;
    public Button StartBtn;
    public TMP_Text Warningtxt;
    void Start()
    {
        sM = ScenesManager.GetInstance();
        inputFieldPanel.SetActive(false);
        NextBtn.onClick.AddListener(InputFieldPanelOn);
        StartBtn.onClick.AddListener(OnClickStart);
    }

    void InputFieldPanelOn()
    {
        NextBtn.gameObject.SetActive(false);
        inputFieldPanel.SetActive(true);
    }
    void OnClickStart()
    {
        if (inputField.text != null)
        {
            Warningtxt.gameObject.SetActive(false);
            inputField.text = playerName;
            PlayerPrefs.SetString("PlayerName", playerName);
            ScenesManager.GetInstance().ChangeScene(Scenes.MenuScene);
        }
        else
        { 
            Warningtxt.gameObject.SetActive(true); 
        }
    }
}
