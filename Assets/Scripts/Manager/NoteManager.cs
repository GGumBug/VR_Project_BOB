using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    #region Singletone

    private static NoteManager instance = null;

    public static NoteManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@NoteManager");
            instance = go.AddComponent<NoteManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    int curNoteTime;

    public List<NoteObject> notes = new List<NoteObject>();
    public List<GameObject> guides = new List<GameObject>();

    public readonly Vector3[] linpos =
    {
        new Vector3(-4f, 1f),
        new Vector3(-1.5f, -1.5f),
        new Vector3(0f, 1f),
        new Vector3(1.5f, -1.5f),
        new Vector3(4f, 1f),
    };

    int next = 0;
    int prev = 0;

    private void Start()
    {
        SetCreateTime(SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic], next);
        StartCoroutine(IEGenTimer(SheetManager.GetInstance().sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].BarPerMilliSec * 0.001f));
    }

    void SetCreateTime(string title, int a)
    {
        if (next == SheetManager.GetInstance().sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].notes.Count)
        {
            Debug.Log("노트 없음");
            return;
        }
        curNoteTime = SheetManager.GetInstance().sheets[title].notes[a].time;
    }

    IEnumerator IEGenTimer(float interval)
    {
        while (true)
        {
            if (next == SheetManager.GetInstance().sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].notes.Count)
            {
                break;
            }
            Gen(SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]);
            yield return new WaitForSeconds(interval / 64);
        }
    }

    void Gen(string title)
    {
        if (curNoteTime < AudioManager.GetInstance().GetMilliSec())
        {
            NoteObject note = ObjectPoolManager.GetInstance().GetNote();
            note.SetPosition(linpos[SheetManager.GetInstance().sheets[title].notes[next].line]);
            note.transform.localScale = new Vector3(0f, 0f, 0f);
            note.life = true;
            notes.Add(note);
            next++;
            SetCreateTime(title, next);
            StartCoroutine("Jugement");
        }
    }

    IEnumerator Jugement()
    {
        NoteObject note = notes[prev];
        CreateGuide(note);
        StartCoroutine(GrowBigNote(note));
        prev = next;
        yield return new WaitForSeconds(SheetManager.GetInstance().sheets[SheetManager.GetInstance().title[SheetManager.GetInstance().curMusic]].BarPerMilliSec * 0.001f);
        if (note != null)
        {
            note.life = false;
            ObjectPoolManager.GetInstance().ReturnObject(note);
        }
    }

    IEnumerator GrowBigNote(NoteObject note)
    {
        while (note.transform.lossyScale.x < 1)
        {
            note.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);
            yield return null;
        }
    }

    void CreateGuide(NoteObject noteObject)
    {
        GameObject go = ObjectPoolManager.GetInstance().GetGuide();
        go.transform.position = noteObject.transform.position;
        guides.Add(go);
    }
}
