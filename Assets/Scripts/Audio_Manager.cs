using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public AudioClip[] audioList;
    public AudioSource AS;

    public void playSound(int index, float pitch)
    {
        AS.pitch = pitch;
        AS.PlayOneShot(audioList[index]);
    }
}