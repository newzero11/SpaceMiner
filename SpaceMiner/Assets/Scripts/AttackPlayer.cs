using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField]
    private GameObject LeftEye;

    [SerializeField]
    private GameObject RightEye;

    [SerializeField]
    private GameObject Eyes;

    [SerializeField]
    private GameObject LaserObject;

    [SerializeField]
    private Material RedEyeMaterial;

    private Ray LeftRay;
    private Ray RightRay;
    public bool AttackMode = false;
    private SkinnedMeshRenderer eyeRenderer;
    private int AlienHealth;
    private bool isLeft = true;
    private bool isShoot = false;
    private GameObject player;
    private Transform shootLaserTransform;
    private Material OriginalEyeMaterial;
    private AudioSource audioSource;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        eyeRenderer = Eyes.GetComponent<SkinnedMeshRenderer>();
        OriginalEyeMaterial = eyeRenderer.material;
        AlienHealth = 100;
        player = GameObject.FindWithTag("Player");
        shootLaserTransform = LaserObject.transform;
    }

    void Update()
    { 
        LeftRay = new Ray(LeftEye.transform.position, transform.localRotation * Vector3.forward * 10f);
        RightRay = new Ray(RightEye.transform.position, transform.localRotation * Vector3.forward * 10f);
        //Debug.DrawRay(LeftEye.transform.position, LeftEye.transform.forward*20f, Color.red);
        //Debug.DrawRay(RightEye.transform.position, RightEye.transform.forward * 20f, Color.red);
        if (isPlayerDetected() == true && !AttackMode) {
            AttackMode = true;
            GetComponent<SetAlienDestination>().AttackMode = true;
            eyeRenderer.material = RedEyeMaterial;

            StartCoroutine(LaserAttack());
        }       

    }

    private bool isPlayerDetected() {
        RaycastHit hit;
        if (Physics.Raycast(LeftRay, out hit)) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                return true;
            }
        }

        if (Physics.Raycast(RightRay, out hit)) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                return true;
            }
        }

        return false;
    }

    IEnumerator LaserAttack() {
        yield return new WaitForSeconds(1f);

        while (GetComponent<ManageAlienHealth>().checkAlienIsDead() != true) {
            ShootLaser(isLeft);
            isLeft = !isLeft;
            yield return new WaitForSeconds(1f);
        }

    }

    private void ShootLaser(bool isLeftTurn) {

        if (isLeftTurn) {
            shootLaserTransform = LeftEye.transform;
        }
        else {
            shootLaserTransform = RightEye.transform;
        }
        audioSource.Play();
        isShoot = true;       
    }

    private void FixedUpdate() {
        if (isShoot) {
            isShoot = false;
            Vector3 pos = shootLaserTransform.position + transform.forward * LaserObject.transform.lossyScale.y;
            Vector3 rot = transform.localRotation.eulerAngles + new Vector3(90, 0, 0);
            GameObject laser = Instantiate(LaserObject, pos, Quaternion.Euler(rot));
            laser.GetComponent<Rigidbody>().velocity = transform.localRotation * Vector3.forward * 6f;
            Destroy(laser,2f);
        }

    }

    public void changeToOriginalEyeMaterial() {
        eyeRenderer.material = OriginalEyeMaterial;
    }
}
