using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour
{
    //Allows for random enemies to spawn on top of the spawners created by the wave manager
    public float timer;
    public Audio_Manager AM;
    public Wave_Manager WM;

    public GameObject Circle;
    public GameObject Hazard;
    public GameObject Shield;
    public ParticleSystem particle;
    private void Start()
    {
        //Time to spawn is slightly randomized for each spawner
        timer = Random.Range(.3f, .8f);
        
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        WM = GameObject.FindWithTag("Game Manager").GetComponent<Wave_Manager>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        //Selects one of the three enemy types to spawn in
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
            
            //Upon spawn the spawner is destroyed and a particle and sound is placed
            Instantiate(particle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            AM.playSound(2);
            
            Destroy(gameObject);
        }
    }
}
