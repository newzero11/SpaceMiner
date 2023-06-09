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

    void Update()
    {
        //When the player's health reaches zero, the game ends.
        if (PlayerHealth<=0 && !isDead) {
            isDead = true;
            StartCoroutine(goToGameOverScene());
        }
    }

    private void OnTriggerEnter(Collider other) {
        //When a player is hit by an alien laser, the health is reduced.
        if (other.gameObject.CompareTag("Laser")) {
            if (!isDead) {
                decreasePlayerHealth(AlienAttackDamage);
            }
        }
    }

    ////When the player dies, go to game over scene.
    IEnumerator goToGameOverScene() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
    //When the player dies, the oxygenerator stops working.
    public bool checkPlayerIsDead() {
        return isDead;
    }

    //Reduce player health and set the value of hp bar accordingly.
    //Show different colors on the hp bar depending on the amount of health left through gradient.
    public void decreasePlayerHealth(int decrement) {
        PlayerHealth -= decrement;
        slider.value = PlayerHealth;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
