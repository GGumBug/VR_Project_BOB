using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int score {get; private set;}
    public int maxHp {get; private set;}
    public int hp {get; private set;}
    public int combo {get; private set;}
    public int perfectCount { get; private set; }
    public int goodCount { get; private set; }
    public int badCount { get; private set; }
    public int missCount { get; private set; }
    public string rank { get; private set; }

    public Player(int score, int maxHp,int hp, int combo, int perfectCount, int goodCount, int badCount, int missCount, string rank)
    {
        this.score = score;
        this.maxHp = maxHp;
        this.hp = hp;
        this.combo = combo;
        this.perfectCount = perfectCount;
        this.goodCount = goodCount;
        this.badCount = badCount;
        this.missCount = missCount;
        this.rank = rank;
    }

    public void PlusHP(int plusHp)
    {
        combo++;
        hp += plusHp;
        Mathf.Clamp(hp, 0, 100);
    }

    public void MinusHP(int minusHp)
    {
        combo = 0;
        hp -= minusHp;
        Mathf.Clamp(hp, 0, 100);
    }

    public void PlusScore(int plusScore)
    {
        score += plusScore;
    }

    public void ResetPlayer()
    {
        score = 0;
        hp = 100;
        combo = 0;
    }

    public void CountCheck(int Count)
    {
        if (Count == 0)
        {
            perfectCount++;
            Debug.Log("퍼펙트 = " + perfectCount);
        }

        else if (Count == 1)
        {
            goodCount++;
            Debug.Log("굿 = " + goodCount);
        }


        else if (Count == 2)
        {
            badCount++;
            Debug.Log("배드 = " + badCount);
        }
        else if (Count == 3)        
        {
            missCount++;
            Debug.Log("미스 = " + missCount);
        }
    }

/*    public void ComboUse(int plusScore)
    {

        if (combo >= 100)
        {
            combo--;
            FeverTime(plusScore);
        }
        else if (combo == 0)
        { 
        
        }
    }
    IEnumerator FeverTime(int plusScore)
    {
        yield return combo == 0;
        {
            PlusScore(plusScore * 2);
        }
    }*/
}
