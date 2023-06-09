using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createMineral : MonoBehaviour
{

    public GameObject mineral1, mineral2, mineral3, materialR;
    private GameObject thisType;
    public AudioSource audioSource;
    public AudioClip audioClip;
    int leftHit = 3;

    void Start() //Initialize the count and collection information of minerals and materials
    {
        GameManager.instance.materialCount = 0;
        GameManager.instance.mineralCount = 0;

        for (int i = 0; i < 12; i++) {
            GameManager.instance.checkM[i] = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pickaxe")) {

            leftHit -= 1;
            if (leftHit <= 0) { //If you hit the rock a certain number of times, it breaks and creates a certain mineral or material 
                if (thisType != null) {
                    
                    Instantiate(thisType, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
                }
                audioSource.PlayOneShot(audioClip);
                Destroy(gameObject);
            }
        }
    }

    public void setType(int num) { //Setting up minerals or materials that will come out of the rock
        switch (num) {
            case 0:
                thisType = null;
                break;
            case 1:
                thisType = mineral1;
                break;
            case 2:
                thisType = mineral2;
                break;
            case 3:
                thisType = mineral3;
                break;
            case 4:
                thisType = materialR;
                break;
            default:
                thisType = null;
                break;
        }
    }

}
