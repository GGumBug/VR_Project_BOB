using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITargetInteface
{
    [SerializeField] ParticleSystem targetBoomPS;

    void Awake()
    { targetBoomPS.Stop(); }

    public void TargetShot()
    {
        TargetShotPS();
      
    }

    public void TargetShotPS()
    {
        targetBoomPS.Play();
    }


}
 