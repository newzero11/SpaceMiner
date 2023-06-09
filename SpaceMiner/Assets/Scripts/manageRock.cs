using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageRock : MonoBehaviour
{
    //To cause random minerals or materials to come out of the rock

    private List<GameObject> rockList = new List<GameObject>();   

    private enum Mtype { 
        none, min1, min2, min3, matR
    }
    
    void Start()
    {
        int count = this.transform.childCount;
        for (int i = 0; i < count; i++) {
            rockList.Add(this.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < 3; j++) { //Randomly select rocks and assign the first three to mineral 1, the second three to mineral 2, and third three to mineral 3
            for (int k = 0; k < 3; k++) {
                int num = Random.Range(0, rockList.Count);
                Mtype curType;
                switch (j) {
                    case 0:
                        curType = Mtype.min1;
                        break;
                    case 1:
                        curType = Mtype.min2;
                        break;
                    case 2:
                        curType = Mtype.min3;
                        break;
                    default:
                        curType = Mtype.none;
                        break;
                }

                rockList[num].GetComponent<createMineral>().setType((int)curType);
                rockList.RemoveAt(num);
            }
        }

        //Assigning material to random one of the remaining
        int num2 = Random.Range(0, rockList.Count);
        rockList[num2].GetComponent<createMineral>().setType((int)Mtype.matR);
        rockList.RemoveAt(num2);


        //Set what is left to empty rock
        foreach (GameObject obj in rockList)
        {
            obj.GetComponent<createMineral>().setType((int)Mtype.none);
        }

    }
}
