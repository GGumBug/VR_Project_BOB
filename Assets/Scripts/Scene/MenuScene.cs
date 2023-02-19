using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    private void Awake()
    {
        RankSystem.GetInstance().StartRankSystem();
    }
}
