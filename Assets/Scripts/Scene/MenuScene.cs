using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < SheetManager.GetInstance().title.Length; i++)
        {
            SheetManager.GetInstance().Init(SheetManager.GetInstance().title[i]);
            PaserManager.GetInstance().Paser(SheetManager.GetInstance().title[i]);
        }
    }
}
