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
    Coroutine coroutine;

    public List<NoteObject> notes = new List<NoteObject>();
    public List<GameObject> guides = new List<GameObject>();
    public List<Coroutine> noteCoroutines = new List<Coroutine>();

    public readonly Vector3[] linpos =
    {
        new Vector3(-3f, .5f),
        new Vector3(-1.2f, -.5f),
        new Vector3(0f, .5f),
        new Vector3(1.2f, -.5f),
        new Vector3(3f, .5f),
    };

    int next = 0;
    int prev = 0;

    private void Start()
    {
        SetCreateTime(SheetManager.GetInstance().GetCurrentTitle(), next);
        StartCoroutine(IEGenTimer(SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].BarPerMilliSec * 0.001f));
    }

    void SetCreateTime(string title, int a)
    {
        if (next == SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].notes.Count)
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
            Gen(SheetManager.GetInstance().GetCurrentTitle());
            yield return new WaitForSeconds(interval / 64);
        }
    }

    void Gen(string title)
    {
        if (curNoteTime < AudioManager.GetInstance().GetMilliSec())
        {
            int ran = Random.Range(0, 2);

            NoteObject note = ObjectPoolManager.GetInstance().GetNote();
            note.note = SheetManager.GetInstance().sheets[title].notes[next];
            note.SetPosition(linpos[note.note.line]);
            //CreateGuide(note);

            note.noteNumber = next;
            note.life = true;
            notes.Add(note);
            next++;
            SetCreateTime(title, next);
            coroutine = StartCoroutine("Jugement");
            noteCoroutines.Add(coroutine);
        }
    }

    IEnumerator Jugement()
    {
        NoteObject note = notes[prev];
        Transform[] model = note.GetComponentsInChildren<Transform>();
        model[1].localScale = new Vector3(0f, 0f, 0f);
        StartCoroutine(GrowBigNote(model[1]));
        prev = next;
        yield return new WaitForSeconds(SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].BarPerMilliSec * 0.001f);
        if (note != null)
        {
            note.life = false;
            ObjectPoolManager.GetInstance().ReturnObject(note);
            GameManager.GetInstance().Miss(); // 미스 판정
        }
    }

    public void StopNoteCoroutine(NoteObject note) // 특정 코루틴 찾아서 스톱하는 함수
    {
        StopCoroutine(noteCoroutines[note.noteNumber]);
    }

    IEnumerator GrowBigNote(Transform model)
    {
        while (model.lossyScale.x < 0.8)
        {
            model.localScale += new Vector3(0.002f, 0.002f, 0.002f);
            yield return null;
        }
    }

    //void CreateGuide(NoteObject noteObject)
    //{
    //    GameObject go = ObjectPoolManager.GetInstance().GetGuide();
    //    go.transform.position = noteObject.transform.position;
    //    guides.Add(go);
    //}
}
