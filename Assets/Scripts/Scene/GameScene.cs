using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        SheetManager.GetInstance().Init(SheetManager.GetInstance().title);
        PaserManager.GetInstance().Paser(SheetManager.GetInstance().title);
        SheetManager.GetInstance().sheets[SheetManager.GetInstance().title].Init();

        AudioManager.GetInstance().InitClip(SheetManager.GetInstance().title);
        AudioManager.GetInstance().progressTime = 0f;
        AudioManager.GetInstance().Play();

        NoteManager.GetInstance().StartGen();
    }
}
