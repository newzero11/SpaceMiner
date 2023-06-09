using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBtn : MonoBehaviour
{
    public void gameClear() { //change to Game Clear scene
        SceneManager.LoadScene("GameClear");
    }

    public void closeInventory() { //remove inventroy
        Destroy(gameObject);
    }
    public void yesBtn() //return to Main Menu
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void noBtn() //remove checkHome Pop-up
    {
        Destroy(gameObject);
    }
}
