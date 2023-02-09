using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunFire : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] Transform gunraycastOrigin;
    [SerializeField] LayerMask targetLayer;

    [Space(10f)]
    [Header("Audio")]
    AudioSource gunAudioSource;
    [SerializeField] AudioClip gunAudioClip;

    private void Awake()
    {
        if (TryGetComponent(out AudioSource audio))
        {
            gunAudioSource = audio;
        }
        else
        {
            gunAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {        
       
    }

    public void ShotRay()
     {
        RaycastHit hit;
       
        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            if (hit.transform.GetComponent<ITargetInteface>() != null)
            {
                hit.transform.GetComponent<ITargetInteface>().TargetShot();

                Debug.Log($"<color=green> hit target {hit.transform.name}</color>");
            }
            else
            { Debug.Log("그걸 못맞추네...."); }

        }

        gunAudioSource.PlayOneShot(gunAudioClip);
      
     }
}
