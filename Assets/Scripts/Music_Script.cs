using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Music_Script : MonoBehaviour
{
    public AudioClip[] audioList;
    public AudioSource AS;

    //Plays a random song on game start
    public void Start()
    {
        int random = Random.Range(0, audioList.Length - 1);
        AS.clip = audioList[random];
        AS.Play();
    }
    //If song is over, chooses and plays a random song
    private void Update()
    {
        if (!AS.isPlaying)
        {
            int random = Random.Range(0, audioList.Length - 1);
            AS.clip = audioList[random];
            AS.Play();
        }
    }
}
