using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    Camera VRCam;

    void Awake()
    {
        UIManager uimanager = UIManager.GetInstance();
        
        VRCam = Camera.main;        
        uimanager.OpenUI("TitleUI");
        TitleUI titleUI = uimanager.GetUI("TitleUI").GetComponent<TitleUI>();
        titleUI.transform.SetParent(VRCam.transform);
        titleUI.name = "TitleUI";
        AudioManager soudnplayer = AudioManager.GetInstance();
        // soudnplayer.PlayBgm("Start");
    }
}
