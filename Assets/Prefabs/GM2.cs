using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2 : MonoBehaviour
{
   static GM2 instance;

    public static GM2 Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }


}
