using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{
    public XRInteractorLineVisual lineVisual; // Reference to the XRInteractorLineVisual component

    private XRController teleportController; // Reference to the XRController for teleportation
    private bool isTeleporting = false;

    void Start()
    {
        // Find the XRControllers for teleportation at runtime
        XRController[] xrControllers = FindObjectsOfType<XRController>();

        foreach (XRController controller in xrControllers)
        {
            if (controller.inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                teleportController = controller;
                break;
            }
        }

        if (teleportController == null)
        {
            Debug.LogWarning("TeleportationController: Left XRController not found.");
        }

        // Disable the teleportation ray by default
        lineVisual.enabled = false;
    }

    void Update()
    {
        if (teleportController == null)
            return;

        // Check if the teleport button ("X" button) is pressed
        if (teleportController.inputDevice.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressed))
        {
            if (!isTeleporting)
            {
                // Show the teleportation ray
                lineVisual.enabled = true;
                isTeleporting = true;
            }
        }
        else if (isTeleporting)
        {
            // Hide the teleportation ray
            lineVisual.enabled = false;
            isTeleporting = false;
        }
    }
}
