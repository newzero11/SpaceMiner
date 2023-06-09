using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class LocomotionController : MonoBehaviour
{
    [SerializeField]
    private ActionBasedController leftTeleportRay;

    [SerializeField]
    private ActionBasedController rightTeleportRay;

    [SerializeField]
    private float teleportActivationThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
        }
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    private bool CheckIfActivated(ActionBasedController controller)
    {
        float value = controller.selectAction.action.ReadValue<float>();
        return value > teleportActivationThreshold;
    }
}
