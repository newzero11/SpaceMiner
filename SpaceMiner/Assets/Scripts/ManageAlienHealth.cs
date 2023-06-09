using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageAlienHealth : MonoBehaviour
{
    private int AlienHealth = 50;
    private bool isAlienDead = false;
    private Animator animator;
    [SerializeField]
    private GameObject HealthBar;

    private Slider slider;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void Start() {
        slider = HealthBar.GetComponent<Slider>();
    }

    private void Update() {
        //When an alien is attacked, it activates the hp bar and reduces the alien's health.
        //The value of the slider is also adjusted to fit the health.
        if (AlienHealth <= 0 && !isAlienDead) {
            isAlienDead = true;
            HealthBar.SetActive(false);
            Debug.Log("Alien Died");
            animator.SetTrigger("isDead");
            StartCoroutine(destroyAlien());
        }

    }

    //Attack from a laser gun, and call this function if an alien is hit,
    //reducing the alien's health.
    public void takeDamage() {
        if (!isAlienDead) {
            HealthBar.SetActive(true);
            AlienHealth -= 10;
            slider.value = AlienHealth;
            Debug.Log("Alien Health: " + AlienHealth);
        }
    }

    //This function is used to determine if aliens are dead in SetAlienDestination.
    public bool checkAlienIsDead() {
        return isAlienDead;
    }

    //If the alien are dead, play related animation and destroy the alien.
    IEnumerator destroyAlien() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
