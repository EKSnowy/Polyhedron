using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
    // From: https://youtu.be/YNJM7rWbbxY?si=xKeOcb88RfijjS3s //
    
    [Header("Values")]
    public string type;
    public float speed;
    private float timer;
    public float bulletLife;
    
    [Header("Everything Else")]
    public Vector2 spawnPoint;
    public ParticleSystem particle;
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        if(timer > bulletLife) 
            Death();
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    public void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Death();
        }
    }
    
    private Vector2 Movement(float timer) {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }
}
