using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartTrigger : MonoBehaviour
{
    public int materialNum;

    public void gravityTrue() { 
        GetComponent<Rigidbody>().useGravity = true; //when player take out the heart from inventory, make it use gravity;
        Physics.IgnoreLayerCollision(0, 7, false); //Enables conflict with the map and spacecraft again.
    }
}
