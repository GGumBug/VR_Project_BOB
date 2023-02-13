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

    PlayerUI playerUI = null;

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

    public Player player = new Player(0, 100, 100, 0);

    public void CheckJugement(NoteObject note, float curtime)
    {
        if (1500 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("BAD");
            player.PlusHP(1);
            player.PlusScore(20);
            RefreshPlayerInfo();
        }
        else if (1200 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("GOOD");
            player.PlusHP(5);
            player.PlusScore(50);
            RefreshPlayerInfo();
        }
        else if (800 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("PERFACT");
            player.PlusHP(10);
            player.PlusScore(100);
            RefreshPlayerInfo();
        }
    }

    public void Miss()
    {
        Debug.Log("MISS");
        player.MinusHP(10);
        RefreshPlayerInfo();
    }

    int GetPerfectTiming(NoteObject note)
    {
        return note.note.time + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset;
    }

    void RefreshPlayerInfo()
    {
        if (playerUI == null)
        {
            playerUI = UIManager.GetInstance().GetUI("PlayerUI").GetComponent<PlayerUI>();
        }
        playerUI.SetPlayerInfo();
    }
}
