using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.HID;
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
    [SerializeField] ParticleSystem gunFirePS;
    [SerializeField] ParticleSystem mGunFirePS;
    [SerializeField] ParticleSystem gunSwapPS;

    //피격, 에임
    [SerializeField] GameObject targetPS;
    [SerializeField] GameObject aimGO;


    [Space(10f)]
    [Header("Audio")]
    AudioSource gunAudioSource;
    [SerializeField] AudioClip gunAudioClip;
    [SerializeField] AudioClip mGunAudioClip;

    [Header("GunModel")]
    [SerializeField] GameObject pistolLeft;
    [SerializeField] GameObject pistolRight;
    [SerializeField] GameObject m_GunLeft;
    [SerializeField] GameObject m_GunRight;

    float pistolshotDleay = 0.5f;
    float m_GunDleay = 0.15f;
    float m_GunDamege = 0.2f;
    bool isLeftShot;
    bool isRightShot;
    bool isM_LeftShot;
    bool isM_RightShot;
    Coroutine leftDleay;
    Coroutine rightDelay;
    Coroutine m_leftDleay;
    Coroutine m_rightDelay;

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
        gunFirePS.Stop();
        mGunFirePS.Stop();
        gunSwapPS.Stop();

        Instantiate(aimGO, new Vector3 (0,0,-5), Quaternion.identity);

    }

    private void Update()
    {
        switch (controller)
        {
            case Controller.Left:
                if (InputManager.GetInstance()._leftController.TryGetFeatureValue(CommonUsages.gripButton, out bool leftgripValue) && leftgripValue && !isM_LeftShot)
                {
                    if (isM_LeftShot)
                        return;
                    isM_LeftShot = true;
                    ChangeGun();
                    InputManager.GetInstance()._leftController.SendHapticImpulse(0, 0.8f, 0.2f);
                    M_ShotRayLeft();
                }
                break;
            case Controller.Right:
                if (InputManager.GetInstance()._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool rightgripValue) && rightgripValue && !isM_RightShot)
                {
                    if (isM_RightShot)
                        return;
                    isM_RightShot = true;
                    ChangeGun();
                    InputManager.GetInstance()._rightController.SendHapticImpulse(0, 0.8f, 0.2f);
                    M_ShotRayRight();
                }
                break;
            default:
                break;
        }

        switch (controller)
        {
            case Controller.Left:
                if (InputManager.GetInstance()._leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue) && leftTriggerValue && !isLeftShot)
                {
                    if (isLeftShot)
                        return;
                    isLeftShot = true;
                    ChangeGun();
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
                    ChangeGun();
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
                    ChangeGun();
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
                    ChangeGun();
                    InputManager.GetInstance()._rightController.SendHapticImpulse(0, 0.8f, 0.2f);
                    ShotRayRight();
                }
                break;
            default:
                break;
        }

        switch (controller)
        {
            case Controller.Left:
                if (Input.GetKey("n") && !isM_LeftShot)
                {
                    if (isM_LeftShot)
                        return;
                    isM_LeftShot = true;
                    ChangeGun();
                    InputManager.GetInstance()._leftController.SendHapticImpulse(0, 0.8f, 0.2f);
                    M_ShotRayLeft();
                }
                break;
            case Controller.Right:
                if (Input.GetKey("m") && !isM_RightShot)
                {
                    if (isM_RightShot)
                        return;
                    isM_RightShot = true;
                    ChangeGun();
                    InputManager.GetInstance()._rightController.SendHapticImpulse(0, 0.8f, 0.2f);
                    M_ShotRayRight();
                }
                break;
            default:
                break;
        }

        RaycastAim();
    }
    
    // 피격 효과
    public void RaycastAim()
    {
       RaycastHit hit;

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            aimGO.transform.position = hit.point;
        }

    }

    public void ShotRayLeft()
    {
        RaycastHit hit;
        gunFirePS.Play();
        gunAudioSource.PlayOneShot(gunAudioClip);

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheckLeft(hit);
        }

        if (leftDleay != null)
        {
            StopCoroutine(leftDleay);
        }
        leftDleay = StartCoroutine("ShotDleayLeft");

    }

    public void M_ShotRayLeft()
    {
        RaycastHit hit;
        mGunFirePS.Play();
        gunAudioSource.PlayOneShot(mGunAudioClip);

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            M_TargetCheckLeft(hit);

        }

        if (m_leftDleay != null)
        {
            StopCoroutine(m_leftDleay);
        }
        m_leftDleay = StartCoroutine("M_GunDleayLeft");
    }

    public void ShotRayRight()
    {
        RaycastHit hit;
        gunFirePS.Play();
        gunAudioSource.PlayOneShot(gunAudioClip);

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            TargetCheckRight(hit);
        }

        if (rightDelay != null)
        {
            StopCoroutine(rightDelay);
        }
        rightDelay = StartCoroutine("ShotDleayRight");
    }

    public void M_ShotRayRight()
    {
        RaycastHit hit;
        mGunFirePS.Play();
        gunAudioSource.PlayOneShot(mGunAudioClip);

        if (Physics.Raycast(gunraycastOrigin.position, gunraycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            M_TargetCheckRight(hit);
        }

        if (m_rightDelay != null)
        {
            StopCoroutine(m_rightDelay);
        }
        m_rightDelay = StartCoroutine("M_GunDleayRight");
    }


    void TargetCheckLeft(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;

            if (hitTarget.gameObject.layer == 5 || hitTarget.gameObject.layer == 6)
            {
                GameObject noteBoomGOclone = Instantiate(targetPS, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(noteBoomGOclone, 2f);
            }

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();

                if (note.controllerType == 0)
                    return;
                if (note.note.type == 1)
                    return;
                ObjectPoolManager.GetInstance().ReturnObject(note);               
                GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                NoteManager.GetInstance().StopNoteCoroutine(note);

            }

        }
        else
        { Debug.Log("NOPE"); }

    }

    void M_TargetCheckLeft(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;
            
            if (hitTarget.gameObject.layer == 5 || hitTarget.gameObject.layer == 6)
            {
                GameObject noteBoomGOclone = Instantiate(targetPS, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(noteBoomGOclone, 2f);
            }
            
            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();

                //if (note.controllerType == 0)
                //    return;
                if (note.note.type == 0)
                    return;
                note.MinusLongNoteCount(m_GunDamege);
                note.longNoteUI.SetValue(note.longNoteCount);

                if (note.longNoteCount <= 0)
                {
                    ObjectPoolManager.GetInstance().ReturnLongNote(note);
                    GameManager.GetInstance().CheckLongPerfactJugement();
                    NoteManager.GetInstance().StopNoteCoroutine(note);
                }
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

            if (hitTarget.gameObject.layer == 5 || hitTarget.gameObject.layer == 6)
            {
                GameObject noteBoomGOclone = Instantiate(targetPS, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(noteBoomGOclone, 2f);
            }

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();

                if (note.controllerType == 1)
                    return;
                if (note.note.type == 1)
                    return;
                ObjectPoolManager.GetInstance().ReturnObject(note);                
                GameManager.GetInstance().CheckJugement(note, AudioManager.GetInstance().GetMilliSec()); //판정 시스템
                NoteManager.GetInstance().StopNoteCoroutine(note);
            }

        }
        else
        { Debug.Log("NOPE"); }

    }

    void M_TargetCheckRight(RaycastHit hit)
    {
        if (hit.transform.GetComponent<ITargetInteface>() != null)
        {
            hit.transform.GetComponent<ITargetInteface>().TargetShot();

            GameObject hitTarget = hit.collider.gameObject;

            if (hitTarget.gameObject.layer == 5 || hitTarget.gameObject.layer == 6)
            {
                GameObject noteBoomGOclone = Instantiate(targetPS, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(noteBoomGOclone, 2f);
            }

            if (hitTarget.gameObject.layer == 6)
            {
                NoteObject note = hitTarget.GetComponent<NoteObject>();

                //if (note.controllerType == 1)
                //    return;
                if (note.note.type == 0)
                    return;
                note.MinusLongNoteCount(m_GunDamege);
                note.longNoteUI.SetValue(note.longNoteCount);

                if (note.longNoteCount <= 0)
                {
                    ObjectPoolManager.GetInstance().ReturnLongNote(note);
                    note.SetLongNoteCount(note.maxLongNoteCount);
                    GameManager.GetInstance().CheckLongPerfactJugement();
                    NoteManager.GetInstance().StopNoteCoroutine(note);
                }
            }
            else
            { Debug.Log("NOPE"); }

        }
    }

    public void ChangeGun()
    {
        switch (controller)
        {
            case Controller.Left:
                if (isM_LeftShot)
                {
                    m_GunLeft.SetActive(true);
                   // gunSwapPS.Play(); //스왚 효과
                    
                    pistolLeft.SetActive(false);
                   // gunSwapPS.Play(); //스왚 효과
                }
                else if (isLeftShot)
                {
                    m_GunLeft.SetActive(false);
                   // gunSwapPS.Play(); //스왚 효과
                    pistolLeft.SetActive(true);
                }
                break;
           
            case Controller.Right:
                if (isM_RightShot)
                {
                    m_GunRight.SetActive(true);
                    pistolRight.SetActive(false);
                }
                else if (isRightShot)
                {
                    m_GunRight.SetActive(false);
                    pistolRight.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    IEnumerator M_GunDleayLeft()
    {
        yield return new WaitForSeconds(m_GunDleay);
        isM_LeftShot = false;
    }

    IEnumerator M_GunDleayRight()
    {
        yield return new WaitForSeconds(m_GunDleay);
        isM_RightShot = false;
    }

    IEnumerator ShotDleayLeft()
    {
        yield return new WaitForSeconds(pistolshotDleay);
        isLeftShot = false;
    }

    IEnumerator ShotDleayRight()
    {
        yield return new WaitForSeconds(pistolshotDleay);
        isRightShot = false;
    }

}
