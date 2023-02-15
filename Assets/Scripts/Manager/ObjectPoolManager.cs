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


    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject LongnotePrefab;

    Queue<NoteObject> PoolNote_0Object = new Queue<NoteObject>();
    Queue<NoteObject> PoolNote_1Object = new Queue<NoteObject>();
    Queue<NoteObject> PoolLongNote_0Object = new Queue<NoteObject>();

    private void InitializeNote(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            PoolNote_0Object.Enqueue(CreateNewObject(0));
        }
    }

    private void InitializeLongNote(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            PoolLongNote_0Object.Enqueue(CreateNewLongNote(0));
        }
    }

    private NoteObject CreateNewObject(int a)
    {
        notePrefab = Resources.Load<GameObject>($"Objects/Note_{a}");
        GameObject note = Instantiate(notePrefab);
        note.gameObject.SetActive(false);
        return note.GetComponent<NoteObject>();
    }

    private NoteObject CreateNewLongNote(int a)
    {
        LongnotePrefab = Resources.Load<GameObject>($"Objects/LongNote");
        GameObject note = Instantiate(LongnotePrefab);
        note.gameObject.SetActive(false);
        return note.GetComponent<NoteObject>();
    }

    public NoteObject GetNote(int a)
    {
        if (a == 0)
        {
            if (PoolNote_0Object.Count > 0)
            {
                var obj = PoolNote_0Object.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject(a);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }
        if (a == 1)
        {
            if (PoolNote_1Object.Count > 0)
            {
                var obj = PoolNote_1Object.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject(a);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        return null;

    }

    public NoteObject GetLongNote(int a)
    {
        if (a == 0)
        {
            if (PoolLongNote_0Object.Count > 0)
            {
                var obj = PoolLongNote_0Object.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewLongNote(a);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }
        if (a == 1)
        {
            if (PoolLongNote_0Object.Count > 0)
            {
                var obj = PoolLongNote_0Object.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewLongNote(a);
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        return null;

    }

    public void ReturnObject(NoteObject obj)
    {
        if (obj.controllerType == 0)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            PoolNote_0Object.Enqueue(obj);
        }
        if (obj.controllerType == 1)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            PoolNote_1Object.Enqueue(obj);
        }

    }

    public void ReturnLongNote(NoteObject obj)
    {
        if (obj.controllerType == 0)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            PoolLongNote_0Object.Enqueue(obj);
        }
        if (obj.controllerType == 1)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            PoolLongNote_0Object.Enqueue(obj);
        }

    }
}
