using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseCollider : MonoBehaviour
{
    //If all the materials are put in the repair socket, the material cannot be removed again

    private GameObject heart;
    private bool destroyCollider = false;

    private void Update()
    {
        if (GameManager.instance.materialCount == 3 && destroyCollider == false) { //Check that all materials are present in the socket
            if (heart != null) {
                Destroy(heart.GetComponent<Collider>());
            }
            destroyCollider = true;
        }
    }

    private void OnTriggerStay(Collider other) //Set up a heart that will get rid of the collider
    {
        if (other.tag == "material") {
            heart = other.gameObject;
        }
    }
}
