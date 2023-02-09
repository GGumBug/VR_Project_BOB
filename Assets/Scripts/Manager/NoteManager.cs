using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

    public GameObject notePrefab;

    IObjectPool<NoteObject> poolNote;

    public readonly Vector3[] linpos =
    {
        new Vector3(-4f, 1f),
        new Vector3(-1.5f, -1.5f),
        new Vector3(0f, 1f),
        new Vector3(1.5f, -1.5f),
        new Vector3(4f, 1f),
    };

    readonly float defaultInterval = 0.005f;
    public float Interval { get; private set; }

    int currentBar = 3; // 최초 플레이 시 3마디 먼저 생성
    int next = 0;
    int prev = 0;
    public List<NoteObject> toReleaseList = new List<NoteObject>();

    Coroutine coGenTimer;
    Coroutine coReleaseTimer;
    Coroutine coInterpolate;

    public IObjectPool<NoteObject> PoolNoteObject
    {
        get
        {
            if (poolNote == null)
            {
                poolNote = new ObjectPool<NoteObject>(CreateNoteObject, defaultCapacity: 256);
            }
            return PoolNoteObject;
        }
    }

    NoteObject CreateNoteObject()
    {
        GameObject note = Instantiate(notePrefab);
        return note.GetComponent<NoteObject>();
    }

    public void StartGen()
    {
        Interval = defaultInterval * SheetManager.GetInstance().Speed;
        coGenTimer = StartCoroutine(IEGenTimer(SheetManager.GetInstance().sheets[SheetManager.GetInstance().title].BarPerMilliSec * 0.001f)); // 음악의 1마디 시간마다 생성할 노트 오브젝트 탐색
        coReleaseTimer = StartCoroutine(IEReleaseTimer(SheetManager.GetInstance().sheets[SheetManager.GetInstance().title].BarPerMilliSec * 0.001f * 0.5f)); // 1마디 시간의 절반 주기로 해제할 노트 오브젝트 탐색
        //coInterpolate = StartCoroutine(IEInterpolate(0.1f, 4f));
    }

    public void StopGen()
    {
        if (coGenTimer != null)
        {
            StopCoroutine(coGenTimer);
            coGenTimer = null;
        }
        if (coReleaseTimer != null)
        {
            StopCoroutine(coReleaseTimer);
            coReleaseTimer = null;
        }
        ReleaseCompleted();
        //Editor.Instance.objects.transform.position = Vector3.zero;

        toReleaseList.Clear();
        currentBar = 3;
        next = 0;
        prev = 0;
    }

    void Gen()
    {
        List<Note> notes = SheetManager.GetInstance().sheets[SheetManager.GetInstance().title].notes;
        List<Note> reconNotes = new List<Note>();

        for (; next < notes.Count; next++)
        {
            if (notes[next].time > currentBar * SheetManager.GetInstance().sheets[SheetManager.GetInstance().title].BarPerMilliSec)
            {
                break;
            }
        }
        for (int j = prev; j < next; j++)
        {
            reconNotes.Add(notes[j]);
        }
        prev = next;

        float currentTime = AudioManager.GetInstance().GetMilliSec();
        float noteSpeed = Interval * 1000;
        foreach (Note note in reconNotes)
        {
            NoteObject noteObject = null;
            noteObject = PoolNoteObject.Get();
            noteObject.SetPosition(linpos[note.line]); // 노트 위치 지정

            noteObject.speed = noteSpeed;
            noteObject.note = note;
            noteObject.life = true;
            noteObject.gameObject.SetActive(true);
            noteObject.SetCollider();
            noteObject.TimeOver();
            toReleaseList.Add(noteObject);
        }

    }

    IEnumerator IEGenTimer(float interval)
    {
        while (true)
        {
            Gen();
            yield return new WaitForSeconds(interval);
            currentBar++;
        }
    }

    IEnumerator IEReleaseTimer(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Release();
        }
    }

    void Release()
    {
        List<NoteObject> reconNotes = new List<NoteObject>();
        foreach (NoteObject note in toReleaseList)
        {
            if (!note.life)
            {
                if (note is NoteObject)
                    PoolNoteObject.Release(note as NoteObject);

                note.gameObject.SetActive(false);
            }
            else
            {
                reconNotes.Add(note);
            }
        }
        toReleaseList.Clear();
        toReleaseList.AddRange(reconNotes);
    }

    void ReleaseCompleted() //분석
    {
        foreach (NoteObject note in toReleaseList)
        {
            note.gameObject.SetActive(false);

            if (note is NoteObject)
                PoolNoteObject.Release(note as NoteObject);
        }
    }
}
