using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Script : MonoBehaviour
{
    // Code from: https://discussions.unity.com/t/how-do-you-make-enemies-rotate-to-your-position-in-2d/219214/3 //
    public Transform target;
    public float speed;

    public ParticleSystem particle;
    public Audio_Manager AM;

    public float health;
    public float maxHealth;
    public HealthBar_Script HB;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        HB = GetComponentInChildren<HealthBar_Script>();
        
        //Stats are selected based on the enemy type detected through tags
        if (gameObject.tag == "Triangle Enemy")
        {
            speed = Random.Range(2, 5);
            health = 3;
            maxHealth = 3;
        }
        else if (gameObject.tag == "Square Enemy")
        {
            speed = Random.Range(1, 3);
            health = 5;
            maxHealth = 5;
        }
        else if (gameObject.tag == "Circle Enemy")
        {
            health = 3;
            maxHealth = 3;
        }
        
    }

    void Update()
    {
        //If square enemy, only follows if far away from the player
        if (gameObject.tag == "Square Enemy")
        {
            if (Vector3.Distance(transform.position, target.position) > 3f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, 
                    speed * Time.deltaTime);
            }
        }
        //If triangle enemy, always follows player
        else if (gameObject.tag == "Triangle Enemy")
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 
                speed * Time.deltaTime);
        }
        
        //Rotates to look at player
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void Death()
    {
        AM.playSound(1);
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void takeDamage(float num)
    {
        health -= num;
        HB.UpdateHealthBar(health,maxHealth);

        if (health <= 0)
        {
            Death();
        }
    }
}
