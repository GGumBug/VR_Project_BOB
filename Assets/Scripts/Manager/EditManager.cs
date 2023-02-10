using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class EditManager : MonoBehaviour
{
    #region Singletone

    private static EditManager instance = null;
    StringBuilder sbAddress = new StringBuilder();

    public static EditManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@EitorManager");
            instance = go.AddComponent<EditManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    AudioSource bgmPlayer;
    public AudioClip bgmClip;

    private void Start()
    {
        BgmStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Input_0();
        }
        if (Input.GetKeyDown("v"))
        {
            Input_1();
        }
        if (Input.GetKeyDown("g"))
        {
            Input_2();
        }
        if (Input.GetKeyDown("b"))
        {
            Input_3();
        }
        if (Input.GetKeyDown("h"))
        {
            Input_4();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
        if (Input.GetKeyDown("q"))
        {
            Export();
        }
    }

    void BgmStart()
    {
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.clip = bgmClip;
        bgmPlayer.volume = 0.5f;
        bgmPlayer.Play();

    }

    void Pause()
    {
        if (bgmPlayer.isPlaying == true)
        {
            bgmPlayer.Pause();
            Debug.Log(bgmPlayer.time * 1000);
        }
        else
        {
            bgmPlayer.Play();
        }

    }

    public float GetMilliSec()
    {
        return bgmPlayer.time * 1000;
    }

    public void Input_0()
    {
        sbAddress.AppendLine($"{(int)(bgmPlayer.time * 1000)}, 0, 0");
        Debug.Log($"{(int)(bgmPlayer.time * 1000)}, 0, 0");
    }

    public void Input_1()
    {
        sbAddress.AppendLine($"{(int)(bgmPlayer.time * 1000)}, 0, 1");
        Debug.Log($"{(int)(bgmPlayer.time * 1000)}, 0, 1");
    }

    public void Input_2()
    {
        sbAddress.AppendLine($"{(int)(bgmPlayer.time * 1000)}, 0, 2");
        Debug.Log($"{(int)(bgmPlayer.time * 1000)}, 0, 2");
    }

    public void Input_3()
    {
        sbAddress.AppendLine($"{(int)(bgmPlayer.time * 1000)}, 0, 3");
        Debug.Log($"{(int)(bgmPlayer.time * 1000)}, 0, 3");
    }

    public void Input_4()
    {
        sbAddress.AppendLine($"{(int)(bgmPlayer.time * 1000)}, 0, 4");
        Debug.Log($"{(int)(bgmPlayer.time * 1000)}, 0, 4");
    }

    public void Export()
    {
        Debug.Log(sbAddress);
    }
}
