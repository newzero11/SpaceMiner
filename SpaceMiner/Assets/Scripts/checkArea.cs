using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkArea : MonoBehaviour
{

    private float curTime;
    private bool chcekDangerous = false;
    public GameObject warningPopup;
    private GameObject newPopup;

    private void Update()
    {
        //HP is decreased every 2 seconds when the player goes out of danger zone
        if (chcekDangerous == true)
        {
            curTime += Time.deltaTime;
            if (curTime > 2)
            {
                GetComponent<ManagePlayerHealth>().decreasePlayerHealth(5);
                curTime = 0;
            }
        }
        else {
            curTime = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "warningArea") //When the player leaves the warning area, a warning pop-up is generated
        {
            if (newPopup != null) {
                Destroy(newPopup);
            }
            newPopup = Instantiate(warningPopup, transform.position + new Vector3(0, 1f, 0), transform.rotation);
            newPopup.transform.Translate(Vector3.forward * 2);
            Destroy(newPopup, 10);

        }
        else if (other.tag == "dangerousArea") //Check if the player goes out of the danger zone and decrease HP
        {
            GetComponent<ManagePlayerHealth>().decreasePlayerHealth(50);

            Destroy(newPopup);
            chcekDangerous = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dangerousArea") //Stops HP reduction when player comes back in from danger zone
        {
            chcekDangerous = false;
        }
        else if (other.tag == "warningArea") { //Clear warning pop-up when player comes back into warning area
            Destroy(newPopup);
        }
    }
}
