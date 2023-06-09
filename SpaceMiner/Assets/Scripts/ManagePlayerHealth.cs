using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagePlayerHealth : MonoBehaviour
{
    private int PlayerHealth = 100;
    private int AlienAttackDamage = 10;
    private bool isDead = false;

    [SerializeField]
    private GameObject PlayerHpBar;

    [SerializeField]
    private Image Fill;

    [SerializeField]
    private Gradient gradient;

    private Slider slider;
    void Start()
    {
        slider = PlayerHpBar.GetComponent<Slider>();
        slider.maxValue = PlayerHealth;
        slider.value = PlayerHealth;

        Fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth<=0 && !isDead) {
            isDead = true;
            Debug.Log("Player Died");
            StartCoroutine(goToGameOverScene());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Laser")) {
            if (!isDead) {
                /*PlayerHealth -= AlienAttackDamage;
                slider.value = PlayerHealth;
                Fill.color = gradient.Evaluate(slider.normalizedValue);*/
                decreasePlayerHealth(AlienAttackDamage);
                Debug.Log("Player Health: " + PlayerHealth);
            }
        }
    }

    IEnumerator goToGameOverScene() {
        yield return new WaitForSeconds(2f);
        //go to game over scene
        SceneManager.LoadScene("GameOver");
    }

    public bool checkPlayerIsDead() {
        return isDead;
    }

    public void decreasePlayerHealth(int decrement) {
        PlayerHealth -= decrement;
        slider.value = PlayerHealth;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
