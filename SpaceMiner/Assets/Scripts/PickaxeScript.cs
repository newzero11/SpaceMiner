using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickaxeScript : MonoBehaviour
{
    public GameObject hitParticles; // Particle system for hit effects
    public AudioSource hittingAudio; // Audio source for hitting sound
    private GameObject newParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            if (hitParticles != null)
            {
                newParticle = Instantiate(hitParticles, transform.position, Quaternion.identity);
                Destroy(newParticle, 1);
            }

            if (hittingAudio != null)
            {
                hittingAudio.Play();
            }
        }
    }
}
