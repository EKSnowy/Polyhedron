using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthTimer;
    public bool resetTimer;
    public float speed;
    
    public Transform target;
    public SpriteRenderer SR;
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
            SR.enabled = true;
            
            health = maxHealth;
            resetTimer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            takeDamage(1);
        }
    }
    
    public void takeDamage(float num)
    {
        health -= num;

        if (health <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SR.enabled = false;
        }
    }
}
