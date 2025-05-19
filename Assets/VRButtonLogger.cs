using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRButtonLogger : MonoBehaviour
{
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice leftHand;
    private InputDevice rightHand;

    void Start()
    {
        // Get devices for left and right hands
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        if (devices.Count > 0) leftHand = devices[0];

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0) rightHand = devices[0];
    }

    void Update()
    {
        CheckDeviceButtons(leftHand, "Left");
        CheckDeviceButtons(rightHand, "Right");
    }

    void CheckDeviceButtons(InputDevice device, string hand)
    {
        Debug.Log(" einai ");
        if (!device.isValid) return;
        Debug.Log("den einai ");
        bool value;
        Vector2 axis2D;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out value) && value)
            Debug.Log($"{hand} - Primary Button (A/X) pressed");

        if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out value) && value)
            Debug.Log($"{hand} - Secondary Button (B/Y) pressed");

        if (device.TryGetFeatureValue(CommonUsages.gripButton, out value) && value)
            Debug.Log($"{hand} - Grip Button pressed");

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out value) && value)
            Debug.Log($"{hand} - Trigger Button pressed");

        if (device.TryGetFeatureValue(CommonUsages.menuButton, out value) && value)
            Debug.Log($"{hand} - Menu Button pressed");

        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out value) && value)
            Debug.Log($"{hand} - Thumbstick Clicked");

        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out axis2D) && axis2D.magnitude > 0.1f)
            Debug.Log($"{hand} - Thumbstick Moved: {axis2D}");
    }
}
