using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;


public class GunFire : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] Transform gunraycastOrigin;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] ParticleSystem shootPS;


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
        shootPS.Stop();
    }

    public void ShotRay()
     {
        RaycastHit hit;
        shootPS.Play();

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {    
            
            if (hit.transform.GetComponent<ITargetInteface>() != null)
            {                
                hit.transform.GetComponent<ITargetInteface>().TargetShot();
                
                GameObject hitTarget = hit.collider.gameObject;

                if (hitTarget.gameObject.layer == 6)
                {
                    NoteObject note = hitTarget.GetComponent<NoteObject>();
                    ObjectPoolManager.GetInstance().ReturnObject(note);
                    NoteManager.GetInstance().StopNoteCoroutine(note);
                    GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                }

                // Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            }
            else
            { Debug.Log("NOPE"); } 

        }

        gunAudioSource.PlayOneShot(gunAudioClip);
      
     }
}
