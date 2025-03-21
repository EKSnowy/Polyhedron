using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Script : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthTimer;
    public bool resetTimer;
    public float speed;
    
    public Transform target;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        transform.RotateAround(target.position, Vector3.back, speed * Time.deltaTime);

        if (healthTimer >= 0)
        {
            healthTimer -= Time.deltaTime;
        }
        else
        {
            resetTimer = true;
        }

        if (resetTimer)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            
            health = maxHealth;
            resetTimer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            takeDamage(1);
        }
        
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.takeDamage(999);
            takeDamage(1);
        }
    }

    public void setHealth(float num)
    {
        maxHealth = num;
        health = maxHealth;
    }

    public void takeDamage(float num)
    {
        health -= num;
        healthTimer = 3;

        if (health <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
