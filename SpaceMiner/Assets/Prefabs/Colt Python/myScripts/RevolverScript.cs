using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Mathematics;

public class RevolverScript : MonoBehaviour
{
    public Transform rotRef;
    public Transform gunBarrelEnd;
    public LineRenderer laser;
    [Range(0f, 100f)] public float range;
    public GameObject laserHit;
    public float damageAmount = 10f; // Define the damage amount for shooting enemies

    private Animator animator;
    private float step;

    public AudioSource shootingAudio;
    public AudioSource hittingAudio;

    private XRGrabInteractable grabInteractable; // Reference to the XRGrabInteractable component

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    private Vector3 controllerVelocity;
    private float prevSpeed;
    public float minSpeed = 1f;
   
    void TryInitialize()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + "Swimming controller ready");
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void Start()
    {
        TryInitialize();
        animator = GetComponent<Animator>();

        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;

        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void OnShoot()
    {
        // Check if the gun is currently being grabbed by the player
        if (grabInteractable.isSelected)
        {
            laser.enabled = true;
            RaycastHit hit;

            if (Physics.Raycast(gunBarrelEnd.position, gunBarrelEnd.forward, out hit, range))
            {
                laser.SetPosition(0, gunBarrelEnd.position);
                laser.SetPosition(1, hit.point);

                // Check if the hit object has the "Enemy" tag
                if (hit.collider.CompareTag("Alien"))
                {
                    /*Enemy enemy = hit.collider.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        // Call the TakeDamage() method on the enemy
                        enemy.TakeDamage(damageAmount);
                    }*/
                    hit.collider.GetComponent<ManageAlienHealth>().takeDamage();
                }
            }
            else
            {
                laser.SetPosition(0, gunBarrelEnd.position);
                laser.SetPosition(1, gunBarrelEnd.position + gunBarrelEnd.forward * range);
            }
            PlayShootingSound(); // Play the shooting audio
        }
    }

    private void PlayShootingSound()
    {
        if (shootingAudio != null)
        {
            shootingAudio.Play();
        }
    }

    public void OnReleaseTrigger()
    {
        laser.enabled = false;
    }


    public void OnReloading(float value)
    {

    }

    private void Update()
    {
        //Updating values
        int currentState = animator.GetInteger("currentState");
        float blendValue = animator.GetFloat("blendValue");

        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            //targetDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 deviceVelocityValue);
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerOut);
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripOut);

            //controllerVelocity = deviceVelocityValue;
            //float speed = currentState == 1 ? math.max(deviceVelocityValue.sqrMagnitude, prevSpeed) : deviceVelocityValue.sqrMagnitude;

            if (currentState != 1 && gripOut < 0)
                currentState = 1;
            else if (currentState == 1 && blendValue > 0.99f)
                currentState = 2;
            
            if(triggerOut > 0)
                currentState = 0;

            animator.SetInteger("currentState", currentState);
            //math.clamp(speed, 0, 1);
            //currentState = triggerOut != 0 ? 

            animator.SetFloat("blendValue", currentState == 0 ? triggerOut: math.clamp(gripOut%1.1f, 0, 1));

        }

    }

    private void FixedUpdate()
    {
        Step(Time.fixedDeltaTime);
    }

    private void Step(float t)
    {
        step = t;
    }

    float Remap(float inValue, float iMin, float iMax, float oMin, float oMax)
    {
        float temp = Mathf.InverseLerp(iMin, iMax, inValue);
        return Mathf.Lerp(oMin, oMax, temp); //outValue
    }
}