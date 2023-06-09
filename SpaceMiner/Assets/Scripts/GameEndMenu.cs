using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    //A script used in GameOver, GameClear scene. 
    //Touching the restart button allows you to restart the game from scratch,
    //and touching the Quit button ends the game.
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void Restart() {
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        audioSource.Play();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
