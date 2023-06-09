using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toolMenu : MonoBehaviour
{
    public GameObject checkHome, inventory, lasergun, pickax;
    private GameObject newInventory, newPickax, newLasergun, newCheckHome;

    public void makeLasergun() { //Create a new gun in front of the player, eliminating any existing guns that have been created
        if (newLasergun != null)
        {
            Destroy(newLasergun);
        }
        newLasergun = Instantiate(lasergun, transform.position + new Vector3(0, 0.2f, 0.1f), transform.rotation);
    }

    public void makePickax() { //Create a new pickax in front of the player, eliminating any existing pickax that have been created
        if (newPickax != null)
        {
            Destroy(newPickax);
        }
        newPickax = Instantiate(pickax, transform.position + new Vector3(0, 0.2f, 0.1f), transform.rotation);
    }

    public void makeInventory() { //Create a new inventory in front of the player, eliminating any existing inventory that have been created
        if (newInventory != null) {
            Destroy(newInventory);
        }
        newInventory = Instantiate(inventory, transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
    }

    public void homeBtn() { //Create a new Home Pop-up in front of the player, eliminating any existing Home Pop-up that have been created
        if (newCheckHome != null)
        {
            Destroy(newCheckHome);
        }
        newCheckHome = Instantiate(checkHome, transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
    }
}
