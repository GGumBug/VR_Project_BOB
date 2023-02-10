using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Awake()
    {
        SheetManager.GetInstance().sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].Init();

        AudioManager.GetInstance().InitClip(SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]);
        AudioManager.GetInstance().progressTime = 0f;
        AudioManager.GetInstance().Play();

        ObjectPoolManager.GetInstance();
        NoteManager.GetInstance();
    }
}
