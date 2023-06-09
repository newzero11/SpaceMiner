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
        if (AlienHealth <= 0 && !isAlienDead) {
            isAlienDead = true;
            HealthBar.SetActive(false);
            Debug.Log("Alien Died");
            animator.SetTrigger("isDead");
        }

    }

    public void takeDamage() {
        if (!isAlienDead) {
            HealthBar.SetActive(true);
            AlienHealth -= 10;
            slider.value = AlienHealth;
            Debug.Log("Alien Health: " + AlienHealth);
        }
    }

    public bool checkAlienIsDead() {
        return isAlienDead;
    }

}
