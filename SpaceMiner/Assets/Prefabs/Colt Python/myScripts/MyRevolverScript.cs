using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(InputData))]
public class MyRevolverScript : MonoBehaviour
{
    public bool side;
    public Transform rotRef;
    public Transform gunBarrelEnd;
    public LineRenderer laser;
    [Range(0f, 100f)] public float range;

    //private InputData inputData;
    private Animator animator;

    private float triggerValue;
    private float prevTriggerValue;
    public float triggerThreshold = 0.1f;
    private Vector3 curPos;
    private Vector3 prevPos;
    private Quaternion curRot;
    private Quaternion prevRot;

    private float prevRotAngle;
    private float prevRotAngleNormalized;

    private float drumValue;
    private float step;

    void Start()
    {
        //inputData = GetComponent<InputData>();
        animator = GetComponent<Animator>();
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void OnShoot(float value)
    {
        //triggerValue = value.Get<float>();
    }

    void OnReload(float value)
    {
        //drumValue = value.Get<float>();
    }

    void OnActivateValue(InputValue value)
    {
        triggerValue = value.Get<float>();
    }

    void OnActivate(InputValue value)
    {
        //triggerValue = value.Get<float>();
    }

    void OnPosition(InputValue value)
    {
        curPos = value.Get<Vector3>();
    }

    void OnRotation(InputValue value)
    {
        curRot = value.Get<Quaternion>();
    }

    void OnRotateAnchor(InputValue value)
    {
        drumValue = value.Get<Vector2>().x;
    }

    private void Update()
    {
        //Updating values
        int currentState = animator.GetInteger("currentState");
        float blendValue = animator.GetFloat("blendValue");
        float rotationAngle = Quaternion.Angle(prevRot, curRot); // Calculate the rotation angle
        float rotAngleWorld = Quaternion.Angle(rotRef.rotation, curRot);
        float rotAngleNormalized = Remap(rotationAngle, 0, 45, 0, 1);

        RaycastHit hit;
        if (currentState == 0 && blendValue >= 0.5f && blendValue <= 0.6f) { 
            
            if(Physics.Raycast(gunBarrelEnd.position, gunBarrelEnd.forward, out hit, range))
            {
                laser.SetPosition(0, gunBarrelEnd.position);
                laser.SetPosition(1, hit.point);
            }
            else
            {
                laser.SetPosition(0, gunBarrelEnd.position);
                laser.SetPosition(1, gunBarrelEnd.position + Vector3.forward*range);
            }
        }

        //if ((rotationAngle - prevRotAngle) / step > 0)
        if (currentState != 1 && rotAngleWorld >= 45f)
            animator.SetInteger("currentState", 1);
        else if (currentState != 0 && triggerValue > 0)
            animator.SetInteger("currentState", 0);
        else if (currentState == 1 && blendValue >= 0.5f)
            animator.SetInteger("currentState", 2);

        //Set blendValue depending on state
        if (currentState == 0)
            animator.SetFloat("blendValue", triggerValue / 2);
        //animator.SetFloat("blendValue", rightTrigger/2);
        else if (currentState == 1)
            animator.SetFloat("blendValue", (rotationAngle - prevRotAngle) / step > 0 ? rotAngleNormalized : blendValue);
        //animator.SetFloat("blendValue", Mathf.Abs(drumValue / 2));
        else if (currentState == 2)
            animator.SetFloat("blendValue", drumValue);
        
        prevTriggerValue = triggerValue;
        prevPos = curPos;
        prevRot = curRot;
        prevRotAngle = rotationAngle;
        //prevRotAngleNormalized = rotAngleNormalized;
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
