using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singletone

    private static ObjectPoolManager instance = null;

    public static ObjectPoolManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@ObjectPoolManager");
            instance = go.AddComponent<ObjectPoolManager>();

            //DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    [SerializeField]
    private GameObject notePrefab;
    private GameObject GuidePrefab;

    Queue<NoteObject> PoolNoteObject = new Queue<NoteObject>();

    Queue<GameObject> Guide = new Queue<GameObject>();

    private void Awake()
    {
        InitializeNote(20);
        InitializeGuide(10);
    }

    private void InitializeNote(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            PoolNoteObject.Enqueue(CreateNewObject());
        }
    }

    private void InitializeGuide(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            Guide.Enqueue(CreateNewGuide());
        }
    }

    private NoteObject CreateNewObject()
    {
        notePrefab = Resources.Load<GameObject>("Objects/Note");
        GameObject note = Instantiate(notePrefab);
        note.gameObject.SetActive(false);
        return note.GetComponent<NoteObject>();
    }

    private GameObject CreateNewGuide()
    {
        GuidePrefab = Resources.Load<GameObject>("Particle/Guide");
        GameObject guidePrefab = Instantiate(GuidePrefab);
        guidePrefab.gameObject.SetActive(false);
        return guidePrefab;
    }

    public NoteObject GetNote()
    {
        if (PoolNoteObject.Count > 0)
        {
            var obj = PoolNoteObject.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public GameObject GetGuide()
    {
        if (Guide.Count > 0)
        {
            var obj = Guide.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewGuide();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObject(NoteObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        PoolNoteObject.Enqueue(obj);
    }

    public void ReturnGuide(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        Guide.Enqueue(obj);
    }
}
