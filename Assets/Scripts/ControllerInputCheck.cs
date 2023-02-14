using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputCheck : MonoBehaviour
{
    [SerializeField] InputDevice targetDevice;
    [SerializeField] List<InputDevice> devices = new List<InputDevice>();
    void Start()
    {
        InputDeviceCharacteristics rightControllerCharcteristicvs = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharcteristicvs, devices);
        InputDeviceCharacteristics leftControllerCharcteristicvs = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharcteristicvs, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            Debug.Log("trigger pressed" + triggerValue);
        }
    }
}
