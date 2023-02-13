using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] TMP_Text PerfectCount;
    [SerializeField] TMP_Text GoodCount;
    [SerializeField] TMP_Text BadCount;
    [SerializeField] TMP_Text MissCount;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Rank;
    [SerializeField] Image RankImg;


    // Start is called before the first frame update
    void Start()
    {
        PerfectCount.text = GameManager.GetInstance().player.perfectCount.ToString();
        GoodCount.text = GameManager.GetInstance().player.goodCount.ToString();
        BadCount.text = GameManager.GetInstance().player.badCount.ToString();
        MissCount.text = GameManager.GetInstance().player.missCount.ToString();
        Score.text = GameManager.GetInstance().player.score.ToString();
        RankImg = Resources.Load<Image>($"Image/Rank/{GameManager.GetInstance().player.rank}");
    }

    // Update is called once per frame
    
}
