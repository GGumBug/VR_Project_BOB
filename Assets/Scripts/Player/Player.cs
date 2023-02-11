using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int score;
    public int hp;
    public int combo;

    public Player(int score, int hp, int combo)
    {
        this.score = score;
        this.hp = hp;
        this.combo = combo;
    }
}
