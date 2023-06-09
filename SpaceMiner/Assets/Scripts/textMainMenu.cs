using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textMainMenu : MonoBehaviour
{
    //The command of the main menu screen changes every 6 seconds

    [SerializeField]
    private TextMeshProUGUI command;

    AudioSource audioSource;

    private string[] commandList = {
        "Your mission is to collect\nall the different types of minerals",
        "If you want to use the tool,\n you can take it out through the Tool Menu",
        "Be careful\nWhile you're on a mission,\naliens may attack you",
        "If you've collected all the minerals,\nrepair the spaceship and come back",
        "Repair is simple!\nPut the materials in a specific location\non the spaceship",
        "Good luck :)"};
    private float curTime;
    private int count;

    void Start()
    {
        command = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        curTime = 5;
        count = 0;
    }

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime > 6) { //every 6 second,

            command.text = commandList[count]; //change the command text
            audioSource.Play();
            count += 1;
            if (count >= 6) {
                count = 0; //return to the first command
            }

            curTime = 0;
        }
    }
}
