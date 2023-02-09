using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject targetNote;


    void Start()
    {
        targetNote = GameObject.FindGameObjectWithTag("_Note");
    }

    // Update is called once per frame
    void Update()
    {

    }

}
