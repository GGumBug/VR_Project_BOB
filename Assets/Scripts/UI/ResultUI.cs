using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] Text PerfectCount;
    [SerializeField] Text GoodCount;
    [SerializeField] Text BadCount;
    [SerializeField] Text MissCount;
    [SerializeField] Text Score;
    [SerializeField] Text Rank;
    [SerializeField] Sprite RankImg;


    // Start is called before the first frame update
    void Start()
    {
        PerfectCount.text = GameManager.GetInstance().player.perfectCount.ToString();
        GoodCount.text = GameManager.GetInstance().player.goodCount.ToString();
        BadCount.text = GameManager.GetInstance().player.badCount.ToString();
        MissCount.text = GameManager.GetInstance().player.missCount.ToString();
        Score.text = GameManager.GetInstance().player.score.ToString();
    }

    // Update is called once per frame
    
}
