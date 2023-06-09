using UnityEngine;

public class Rock : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            DestroyRock();
        }
    }

    private void DestroyRock()
    {
        // Perform any necessary actions when the rock is destroyed
        Destroy(gameObject);
    }
}
