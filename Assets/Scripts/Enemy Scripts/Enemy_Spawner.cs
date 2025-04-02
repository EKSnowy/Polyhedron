using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Spawner : MonoBehaviour
{
    //Allows for random enemies to spawn on top of the spawners created by the wave manager
    [Header("Values")]
    public float timer;
    public float invisTimer;
    public bool enemyChosen;
    
    [Header("Objects")]
    public GameObject Circle;
    public GameObject Hazard;
    public GameObject Shield;
    public GameObject Enemy;
    
    [Header("Particle")]
    public ParticleSystem particle;
    
    [Header("Scripts")]
    public Audio_Manager AM;
    public Wave_Manager WM;

    [Header("Colors")]
    public SpriteRenderer SR;
    public Color circleColor;
    public Color squareColor;
    public Color triangleColor;
    public Color invisible;
    
    private void Start()
    {
        //Time to spawn is slightly randomized for each spawner
        timer = Random.Range(.35f, .85f);
        invisTimer = .1f;
        
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        WM = GameObject.FindWithTag("Game Manager").GetComponent<Wave_Manager>();

        //Starts off as invisible
        SR.color = invisible;
    }

    void Update()
    {
        //Selects one of the three enemy types to spawn in
        if (invisTimer <= 0)
        {
            if (!enemyChosen)
            {
                int random = Random.Range(1, 4);

                if (random == 1)
                {
                    Enemy = Circle;
                    SR.color = circleColor;

                }
                else if (random == 2)
                {
                    Enemy = Hazard;
                    SR.color = triangleColor;
                }
                else if (random == 3)
                {
                    Enemy = Shield;
                    SR.color = squareColor;
                }

                enemyChosen = true;
            }

        }
        else
        {
            invisTimer -= Time.deltaTime;
        }
        
        //Spawn timer counts down and then spawns at 0//
        timer -= Time.deltaTime;
        
        if (timer < 0)
        {
            WM.addList(Instantiate(Enemy, new Vector2(transform.position.x, transform.position.y), Quaternion.identity));
            
            //Upon spawn the spawner is destroyed and a particle and sound is placed
            Instantiate(particle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            AM.playSound(2,1);
            
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Boss")
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
        
        if (other.gameObject.tag == "Wall")
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
    }
    
    public void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Boss")
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
        
        if (other.gameObject.tag == "Wall")
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
    }
}
