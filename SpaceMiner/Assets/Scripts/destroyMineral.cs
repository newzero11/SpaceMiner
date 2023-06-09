using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMineral : MonoBehaviour
{
    //When it collides with a bag collider inside the player's body, it removes the mineral or material and adds it to the inventory.

    public int typeNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bag")) {
            if (GameManager.instance.checkM[typeNum] == false) { //Check the duplication
                GameManager.instance.checkM[typeNum] = true; //Mark on array elements corresponding to mineral or material number
                if (typeNum > 2) {
                    GameManager.instance.mineralCount += 1;
                }
            }
            Destroy(gameObject);
        }
    }
}
