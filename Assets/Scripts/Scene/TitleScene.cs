using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    void Awake()
    {
        UIManager.GetInstance().OpenUI("TitleUI");
        UIManager.GetInstance().OpenUI("FadeUI");
        TitleUI titleUI = UIManager.GetInstance().GetUI("TitleUI").GetComponent<TitleUI>();
    }
}
