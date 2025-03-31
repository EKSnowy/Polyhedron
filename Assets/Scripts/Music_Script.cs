using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Music_Script : MonoBehaviour
{
    public AudioClip[] musicList;
    public AudioClip[] bossMusicList;
    public AudioSource AS;
    public bool inBossLevel;
    
    public void Start()
    {
        randomizeSong();
    }
    
    private void Update()
    {
        //If not in boss and music ends, chooses and plays random song//
        if (!inBossLevel)
        {
            if (!AS.isPlaying)
            {
                randomizeSong();
            }
        }
        //If in boss and music ends, play random boss music//
        else if (!AS.isPlaying)
        {
            playBossSong();
        }
    }

    public void randomizeSong()
    {
        inBossLevel = false;
        int random = Random.Range(0, musicList.Length);
        
        AS.clip = musicList[random];
        AS.Play();
    }

    public void playBossSong()
    {
        inBossLevel = true;
        int random = Random.Range(0, bossMusicList.Length);
        
        AS.clip = bossMusicList[random];
        AS.Play();
    }
}
