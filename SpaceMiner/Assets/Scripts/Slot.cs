using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotNum;
    public GameObject slotItem;
    private bool slotStatus = false;
    private GameObject obj;

    void Start() //Show what is already in the inventory when it is created
    {
        if (slotStatus == false && GameManager.instance.checkM[slotNum] == true) {
            if (slotNum < 3)
            {
                slotMat();
            }
            else {
                slotMin();
            }
        }
    }

    private void Update() //To ensure that the inventory is updated even when the inventory is open
    {
        if (slotNum < 3)
        {
            if (slotStatus == false && GameManager.instance.checkM[slotNum] == true)
            {
                slotMat();
            }
        }
        else {
            if (slotStatus == false && GameManager.instance.checkM[slotNum] == true)
            {
                slotMin();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "material") { //Indicates whether the slot is empty or not
            GameManager.instance.checkM[slotNum] = false; //Indicates that the material is not in the inventory
            slotStatus = false;
        }   
    }

    private void slotMat() {
        obj = Instantiate(slotItem, transform); //Create a material corresponding to the slot
        obj.transform.parent = transform.parent.parent;
        obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); //Sizing the material in the inventory
        obj.GetComponent<Rigidbody>().useGravity = false; //Disable useGravity to avoid falling
        Physics.IgnoreLayerCollision(0, 7, true); //Disables conflict with the map and spaceship.
        slotStatus = true;
    }

    private void slotMin() {
        obj = Instantiate(slotItem, transform); //Create a mineral corresponding to the slot
        //Sizing the minerals in the inventory
        if (slotNum < 6)
        {
            obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            obj.transform.position -= new Vector3(0, 0.025f, 0);
        }
        else if (slotNum < 9)
        {
            obj.transform.localScale = new Vector3(30, 30, 30);
            obj.transform.Rotate(new Vector3(-90, 0, 0));
        }
        else if (slotNum < 11)
        {
            obj.transform.localScale = new Vector3(50, 50, 50);
            if (slotNum == 9)
            {
                obj.transform.Rotate(new Vector3(-90, 0, 0));
            }
        }
        else
        {
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.transform.Rotate(new Vector3(-90, 0, 0));
        }

        obj.GetComponent<Rigidbody>().useGravity = false; //Disable useGravity to avoid falling
        Destroy(obj.GetComponent<XRGrabInteractable>()); //To prevent catching minerals in the inventory
        Destroy(obj.GetComponent<BoxCollider>());
        Destroy(obj.GetComponent<Rigidbody>()); 
        slotStatus = true;
    }
}
