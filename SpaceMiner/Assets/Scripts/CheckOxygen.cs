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

        StartCoroutine(decreaseOxygen());
    }

    IEnumerator decreaseOxygen() {
        while (oxygen > 0 && Player.GetComponent<ManagePlayerHealth>().checkPlayerIsDead() != true) {
            yield return new WaitForSeconds(TimeRate);
            oxygen -= 1;

            OxygenText.text = oxygen + "%";

            if (oxygen <= 30) {
                OxygenText.color = Color.red;
            }
        }

        if (oxygen == 0) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
