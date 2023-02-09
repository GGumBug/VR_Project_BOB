using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireCtrl : MonoBehaviour
{
    [SerializeField] GameObject handgun;        

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Bang();
    }

    void Bang()
    {
        if (Vector3.Distance(transform.position, handgun.transform.position) < 2f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Grab & Bang");
            }
        }
        return;
    }
}
