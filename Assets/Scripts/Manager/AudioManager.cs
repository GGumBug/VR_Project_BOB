using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum State
{
    Playing,
    Paused,
    Unpaused,
    Stop,
}

public class AudioManager : MonoBehaviour
{

    #region SingletoneMake
    public static AudioManager instance = null;
    public static AudioManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@AudioManager");
            instance = go.AddComponent<AudioManager>();

            DontDestroyOnLoad(go);
        }
        return instance;
    }
    #endregion

    public State state = State.Stop;

    public AudioSource BgmPlayer;

    public void InitClip(string title)
    {
        BgmPlayer = gameObject.AddComponent<AudioSource>();
        BgmPlayer.clip = SheetManager.GetInstance().sheets[title].clip;
    }
    public void Play()
    {
        state = State.Playing;
        BgmPlayer.volume = 1;
        BgmPlayer.Play();
    }

    public float progressTime
    {
        get
        {
            float time = 0f;
            if (BgmPlayer.clip != null)
                time = BgmPlayer.time;
            return time;
        }
        set
        {
            if (BgmPlayer.clip != null)
                BgmPlayer.time = value;
        }
    }

    public float GetMilliSec()
    {
        return BgmPlayer.time * 1000;
    }

    public void FadeOutBGM()
    {
        StartCoroutine("IEFadeOutBGM");
    }

    IEnumerator IEFadeOutBGM()
    {
        while(BgmPlayer.volume >0)
        {
            BgmPlayer.volume -= 0.001f;
            yield return null;
        }

        if (BgmPlayer.volume == 0)
        {
            BgmPlayer.Stop();
        }

    }
}
