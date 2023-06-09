using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int mineralCount = 0, materialCount = 0;
    public bool[] checkM = new bool[12]; //0~2 = heart, 3~5 = crystal planet, 6~8 = ice planet, 9~11 = nature planet
}
