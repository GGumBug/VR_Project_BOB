using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int score {get; private set;}
    public int maxHp {get; private set;}
    public int hp {get; private set;}
    public int combo {get; private set;}

    public Player(int score, int maxHp,int hp, int combo)
    {
        this.score = score;
        this.maxHp = maxHp;
        this.hp = hp;
        this.combo = combo;
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
        maxHp = 100;
        hp = 100;
        combo = 0;
    }
}
