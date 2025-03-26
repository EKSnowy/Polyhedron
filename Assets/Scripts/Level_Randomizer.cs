using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Randomizer : MonoBehaviour
{
    public GameObject Layout1;
    public GameObject Layout2;
    public GameObject Layout3;
    public GameObject Layout4;

    public float random;
    void Start()
    {
        Layout1.SetActive(false);
        Layout2.SetActive(false);
        Layout3.SetActive(false);
        Layout4.SetActive(false);
        
        random = Random.Range(1, 5);

        if (random == 1)
        {
            Layout1.SetActive(true);
        }
        
        else if (random == 2)
        {
            Layout2.SetActive(true);
        }
        
        else if (random == 3)
        {
            Layout3.SetActive(true);
        }
        
        else if (random == 4)
        {
            Layout4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
