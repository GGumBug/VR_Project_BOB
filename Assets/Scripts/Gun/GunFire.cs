using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public enum Controller
{
    Left,
    Right
}

public class GunFire : MonoBehaviour
{
    [SerializeField] Controller controller;

    [Header("Raycast")]
    [SerializeField] Transform gunraycastOrigin;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] ParticleSystem shootPS;    


    [Space(10f)]
    [Header("Audio")]
    AudioSource gunAudioSource;
    [SerializeField] AudioClip gunAudioClip;

    float shotDleay = 0.5f;
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
        switch (controller)
        {
            case Controller.Left:
                if (InputManager.GetInstance()._leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue) && leftTriggerValue && !isLeftShot)
                {
                    if (isLeftShot)
                        return;
                    isLeftShot = true;
                    InputManager.GetInstance()._leftController.SendHapticImpulse(0, 0.8f, 0.2f);
                    ShotRayLeft();
                }
                break;
            case Controller.Right:
                if (InputManager.GetInstance()._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue) && rightTriggerValue && !isRightShot)
                {
                    if (isRightShot)
                        return;
                    isRightShot = true;
                    InputManager.GetInstance()._rightController.SendHapticImpulse(0, 0.8f, 0.2f);
                    ShotRayRight();
                }
                break;
            default:
                break;
        }
        //PC 디버그 로직
        switch (controller)
        {
            case Controller.Left:
                if (Input.GetMouseButtonDown(0) && !isLeftShot)
                {
                    if (isLeftShot)
                        return;
                    isLeftShot = true;
                    InputManager.GetInstance()._leftController.SendHapticImpulse(0, 0.8f, 0.2f);
                    ShotRayLeft();
                }
                break;
            case Controller.Right:
                if (Input.GetMouseButtonDown(1) && !isRightShot)
                {
                    if (isRightShot)
                        return;
                    isRightShot = true;
                    InputManager.GetInstance()._rightController.SendHapticImpulse(0, 0.8f, 0.2f);
                    ShotRayRight();
                }
                break;
            default:
                break;
        }
    }

    public void ShotRayLeft()
     {
        RaycastHit hit;
        shootPS.Play();

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheckLeft(hit);            
        }

        gunAudioSource.PlayOneShot(gunAudioClip);

        if (leftDleay != null)
        {
            StopCoroutine(leftDleay);
        }
        leftDleay = StartCoroutine("ShotDleayLeft");
     }

    public void ShotRayRight()
    {
        RaycastHit hit;
        shootPS.Play();

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheckRight(hit);
        }

        gunAudioSource.PlayOneShot(gunAudioClip);

        if (rightDelay != null)
        {
            StopCoroutine(rightDelay);
        }
        rightDelay = StartCoroutine("ShotDleayRight");
    }

    void TargetCheckLeft(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();
                Debug.Log($"note.controllerType = {note.controllerType}");
                if (note.controllerType == 0)
                    return;                              
                ObjectPoolManager.GetInstance().ReturnObject(note);
                // 타겟 맞았을 때 파티클 시스템
                GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                NoteManager.GetInstance().StopNoteCoroutine(note);

            }

        }
        else
        { Debug.Log("NOPE"); }

    }

    void TargetCheckRight(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();
                Debug.Log($"note.controllerType = {note.controllerType}");
                if (note.controllerType == 1)
                    return;
                ObjectPoolManager.GetInstance().ReturnObject(note);
                // 타겟 맞았을 때 파티클 시스템
                GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                NoteManager.GetInstance().StopNoteCoroutine(note);
            }

        }
        else
        { Debug.Log("NOPE"); }

    }

    IEnumerator ShotDleayLeft()
    {
        yield return new WaitForSeconds(shotDleay);
        isLeftShot = false;
    }

    IEnumerator ShotDleayRight()
    {
        yield return new WaitForSeconds(shotDleay);
        isRightShot = false;
    }
}
