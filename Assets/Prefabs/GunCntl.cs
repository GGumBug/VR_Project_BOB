using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunCntl : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabInteractable;

    private void OnEnalbe() => grabInteractable.activated.AddListener(TriggerPulled);
    private void OnDisalbe() => grabInteractable.activated.AddListener(TriggerPulled);

    private void TriggerPulled(ActivateEventArgs arg0)
    {
        arg0.interactor.GetComponent<XRBaseController>().SendHapticImpulse(0.5f, 0.25f);
    }

}
