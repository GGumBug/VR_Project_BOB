using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool life;

    public Note note = new Note();

    public float speed = 5f;

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetCollider()
    {
        if (GameManager.GetInstance().state == GameState.Game)
        {
            GetComponent<SphereCollider>().enabled = true;
        }
        //else
        //{
        //    StartCoroutine(IECheckCollier());
        //}
    }

    public void TimeOver()
    {
        StartCoroutine("IETimeOver");
    }

    public IEnumerator IETimeOver()
    {
        yield return new WaitForSeconds(1f);
        life = false;
    }
}
