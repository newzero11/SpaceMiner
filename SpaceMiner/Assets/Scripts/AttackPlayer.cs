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
        player = GameObject.FindWithTag("Player");
        shootLaserTransform = LaserObject.transform;
    }

    void Update()
    {
        //It is a ray used to detect the player.
        LeftRay = new Ray(LeftEye.transform.position, transform.localRotation * Vector3.forward * 10f);
        RightRay = new Ray(RightEye.transform.position, transform.localRotation * Vector3.forward * 10f);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //Check the distance between the player and the alien,
        //and set it to attack mode when it comes within a certain distance.
        if (isPlayerDetected() == true && !AttackMode && distanceToPlayer<8) {
            AttackMode = true;
            //Set Attack Mode of SetAlienDestination to true
            //to change the destination to the player position.
            GetComponent<SetAlienDestination>().AttackMode = true;
            //The alien's eyes turn red when in attack mode.
            eyeRenderer.material = RedEyeMaterial;

            //And start the eye laser attack.
            StartCoroutine(LaserAttack());
        }       

    }

    //Aliens have a ray in their left eye and right eye,
    //and if they detect a player in either ray, they change the attack mode to true.
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

        //The laser is fired alternately in both eyes every second.
        while (GetComponent<ManageAlienHealth>().checkAlienIsDead() != true && AttackMode) {
            ShootLaser(isLeft);
            isLeft = !isLeft;
            yield return new WaitForSeconds(1f);
        }

    }

    //Determines the position to be fired based on which eye it is to be fired from.
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
        //The laser object is instantiated and fired,
        //and the object is destroyed after 2 seconds.
        if (isShoot) {
            isShoot = false;
            Vector3 pos = shootLaserTransform.position + transform.forward * LaserObject.transform.lossyScale.y;
            Vector3 rot = transform.localRotation.eulerAngles + new Vector3(90, 0, 0);
            GameObject laser = Instantiate(LaserObject, pos, Quaternion.Euler(rot));
            laser.GetComponent<Rigidbody>().velocity = transform.localRotation * Vector3.forward * 6f;
            Destroy(laser,2f);
        }

    }

    //When the attack mode changes from true to false,
    //it changes the eye to its original color.
    public void changeToOriginalEyeMaterial() {
        eyeRenderer.material = OriginalEyeMaterial;
    }
}
