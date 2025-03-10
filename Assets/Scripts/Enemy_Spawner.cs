using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour
{
    public float timer;
    public Audio_Manager AM;
    public Wave_Manager WM;

    public GameObject Circle;
    public GameObject Hazard;
    public GameObject Shield;
    public ParticleSystem particle;
    private void Start()
    {
        timer = Random.Range(.3f, .8f);
        
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        WM = GameObject.FindWithTag("Game Manager").GetComponent<Wave_Manager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            int random = Random.Range(1, 4);

            if (random == 1)
            {
                WM.addList(Instantiate(Circle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            }
            else if (random == 2)
            {
                WM.addList(Instantiate(Hazard, new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            }
            else if (random == 3)
            {
                WM.addList(Instantiate(Shield, new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            }
            
            Instantiate(particle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            AM.playSound(2);
            
            Destroy(gameObject);
        }
    }
}
