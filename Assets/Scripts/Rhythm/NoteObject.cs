using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool life;

    public Note note = new Note();

    public float speed = 5f;

    public int noteNumber;

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void TimeOver()
    {
        life = false;
        ObjectPoolManager.GetInstance().ReturnObject(this);
    }
}
