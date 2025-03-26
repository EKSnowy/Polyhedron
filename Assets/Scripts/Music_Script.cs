using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Music_Script : MonoBehaviour
{
    public AudioClip[] audioList;
    public AudioSource AS;

    public void Start()
    {
        int random = Random.Range(0, 3);
        AS.clip = audioList[random];
        AS.Play();
    }
}
