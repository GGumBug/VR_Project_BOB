using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Game,
    Edit,
}

public class GameManager : MonoBehaviour
{
    #region Singletone

    private static GameManager instance = null;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GameManager");
            instance = go.AddComponent<GameManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public GameState state = GameState.Game;

    Player player = new Player(0, 100, 0);

    public void CheckJugement(NoteObject note, float curtime)
    {
        Debug.Log($"GetPerfectTiming(note) = {GetPerfectTiming(note)}");
        Debug.Log($"curtime = {curtime}");
        Debug.Log(GetPerfectTiming(note) - curtime);
        if (1500 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("BAD");
            player.combo++;
            Debug.Log($"COMBO {player.combo}");
            PlusHP(1);
        }
        else if (1200 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("GOOD");
            player.combo++;
            Debug.Log($"COMBO {player.combo}");
            PlusHP(5);
        }
        else if (800 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("PERFACT");
            player.combo++;
            Debug.Log($"COMBO {player.combo}");
            PlusHP(10);
        }
    }

    public void Miss()
    {
        Debug.Log("MISS");
        MinusHP(10);
    }

    int GetPerfectTiming(NoteObject note)
    {
        return note.note.time + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset;
    }

    void PlusHP(int plusHp)
    {
        player.hp += plusHp;
        Mathf.Clamp(player.hp, 0, 100);
    }

    void MinusHP(int minusHp)
    {
        player.combo = 0;
        player.hp -= minusHp;
        Mathf.Clamp(player.hp, 0, 100);
    }

    void ResetPlayer()
    {
        player.score = 0;
        player.hp = 100;
        player.combo = 0;
    }
}
