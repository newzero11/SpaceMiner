using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject main, option, credit;

    public AudioSource audioSource;

    private void Start()
    {
        main.SetActive(true);
        option.SetActive(false);
        credit.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void startBtn() { //change the scene to PlayScene
        audioSource.Play();
        SceneManager.LoadScene("PlayScene");
    }
    public void optionBtn() { //show option screen
        audioSource.Play();
        main.SetActive(false);
        option.SetActive(true);
    }
    public void creditBtn() { //show credit screen
        audioSource.Play();
        main.SetActive(false);
        credit.SetActive(true);
    }
    public void backBtn() { //back to the main menu screen
        audioSource.Play();
        if (option.activeSelf == true) {
            option.SetActive(false);
            main.SetActive(true);
        } else if (credit.activeSelf == true) {
            credit.SetActive(false);
            main.SetActive(true);
        }
    }
}
