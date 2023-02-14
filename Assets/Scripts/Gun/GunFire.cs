using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;


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

    float shotDleay = 1.5f;
    bool isLeftShot;
    bool isRightShot;
    Coroutine leftDleay;
    Coroutine rightDelay;

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

    private void Update()
    {
        Debug.Log(isLeftShot);
        if (InputManager.GetInstance()._leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue) && leftTriggerValue && !isLeftShot)
        {
            if (isLeftShot)
                return;
            isLeftShot = true;
            ShotRayLeft();
        }
        if (InputManager.GetInstance()._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue) && rightTriggerValue && !isRightShot)
        {
            if (isRightShot)
                return;
            ShotRayRight();
        }

        Debug.Log("leftTriggerValue  = " +leftTriggerValue);
    }

    public void ShotRayLeft()
     {
        RaycastHit hit;
        shootPS.Play();

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheck(hit);
        }

        gunAudioSource.PlayOneShot(gunAudioClip);

        if (leftDleay != null)
        {
            StopCoroutine(leftDleay);
        }
        leftDleay = StartCoroutine(ShotDleay(isLeftShot));
     }

    public void ShotRayRight()
    {
        isRightShot = true;
        RaycastHit hit;
        shootPS.Play();

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheck(hit);
        }

        gunAudioSource.PlayOneShot(gunAudioClip);

        if (rightDelay != null)
        {
            StopCoroutine(leftDleay);
        }
        rightDelay = StartCoroutine(ShotDleay(isRightShot));
    }

    void TargetCheck(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();
                ObjectPoolManager.GetInstance().ReturnObject(note);
                GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                NoteManager.GetInstance().StopNoteCoroutine(note);
            }

            // Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
        }
        else
        { Debug.Log("NOPE"); }

    }

    IEnumerator ShotDleay(bool swich)
    {
        yield return new WaitForSeconds(shotDleay);
        swich = false;
    }
}
