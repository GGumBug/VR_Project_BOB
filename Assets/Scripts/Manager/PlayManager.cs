using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    // 타겟 점수 판정
    bool isPerfecthit;
    bool isGoodhit;
    bool isBadhit;
    bool isMisshit;
    bool isTargethit;

    // 점수 관련 변수
    int curScore;
    int bestScore;
    float TimingLate;

    int playerHP;
   


    // Start is called before the first frame update
    void Start()
    {
        CulculateScore();
        CheckGameOver();
        CheckTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckTarget()
    {
        if (isTargethit)
        {
            if (TimingLate <= 90)
                isPerfecthit = true;
            if ( TimingLate < 90 && TimingLate >= 50)
                isGoodhit = true;
            else if (TimingLate >= 10 && TimingLate < 50)
                isBadhit = true;
            
        }
        else
        isMisshit = true;

    }
    void CulculateScore()
    {
        if (isPerfecthit)
        {
            curScore = curScore + 100;
        }
        if (isGoodhit)
        {
            curScore = curScore + 50;
        }
        if (isBadhit)
        {
            curScore = curScore + 10;
        }
        if (isMisshit)
        {
            curScore = curScore - 10;
        }
    }

    
    void CheckGameOver()
    {
        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    { 
        
    }
}
