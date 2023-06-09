using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairSpaceship : MonoBehaviour
{
    public GameObject fire, clearPopupPrefab;
    public Transform firePoint, popupPoint;
    private bool fireStatus = false, clearPopup = false;

    void Update()
    {
        if (fireStatus == false && GameManager.instance.materialCount == 3) { //Fire is created when all of the repair material is inserted into the socket
            fireStatus = true;
            Instantiate(fire, firePoint);
        }

        //Once all kinds of minerals have been collected and the spacecraft has been repaired, create a game clear pop-up
        if (GameManager.instance.materialCount == 3 && GameManager.instance.mineralCount == 9) {
            if (clearPopup == false) {
                Instantiate(clearPopupPrefab, popupPoint.position + new Vector3(0, 0.35f, -0.05f), Quaternion.identity);
                clearPopup = true;
            }
        }
    }

    public void putSocket() {
        GameManager.instance.materialCount += 1;
    }

    public void takeOutSocket() {
        GameManager.instance.materialCount -= 1;
    }
}
