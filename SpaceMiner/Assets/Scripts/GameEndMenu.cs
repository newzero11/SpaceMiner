using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
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
