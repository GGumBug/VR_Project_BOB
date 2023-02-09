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

    Queue<NoteObject> PoolNoteObject = new Queue<NoteObject>();

    private void Awake()
    {
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            PoolNoteObject.Enqueue(CreateNewObject());
        }
    }

    private NoteObject CreateNewObject()
    {
        notePrefab = Resources.Load<GameObject>("Objects/Note");
        GameObject note = Instantiate(notePrefab);
        note.gameObject.SetActive(false);
        return note.GetComponent<NoteObject>();
    }

    public NoteObject GetObject()
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

    public void ReturnObject(NoteObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        PoolNoteObject.Enqueue(obj);
    }
}
