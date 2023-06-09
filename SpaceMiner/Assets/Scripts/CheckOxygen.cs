using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckOxygen : MonoBehaviour
{
    private TextMeshProUGUI OxygenText;
    private int TimeRate;
    private int oxygen;
    private GameObject Player;

    private void Awake() {
        OxygenText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        TimeRate = 20;
        oxygen = 100;
        Player = GameObject.FindWithTag("Player");
        //When the game starts, the oxygen level decreases by 1% every 20 seconds.
        StartCoroutine(decreaseOxygen());
    }

    IEnumerator decreaseOxygen() {
        while (oxygen > 0 && Player.GetComponent<ManagePlayerHealth>().checkPlayerIsDead() != true) {
            yield return new WaitForSeconds(TimeRate);
            oxygen -= 1;

            OxygenText.text = oxygen + "%";

            //If the oxygen level is less than 30%,
            //change the color of the text to red as a warning sign.
            if (oxygen <= 30) {
                OxygenText.color = Color.red;
            }
        }

        //The game ends when the oxygen level reaches zero.
        if (oxygen == 0) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
