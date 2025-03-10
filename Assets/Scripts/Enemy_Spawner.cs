using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public Audio_Manager AM;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
