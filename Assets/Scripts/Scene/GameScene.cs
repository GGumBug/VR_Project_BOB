using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Awake()
    {
        SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].Init();

        AudioManager.GetInstance().InitClip(SheetManager.GetInstance().GetCurrentTitle());
        AudioManager.GetInstance().progressTime = 0f;
        AudioManager.GetInstance().Play();

        UIManager.GetInstance().OpenUI("PlayerUI");

        ObjectPoolManager.GetInstance();
        NoteManager.GetInstance();
    }
}
