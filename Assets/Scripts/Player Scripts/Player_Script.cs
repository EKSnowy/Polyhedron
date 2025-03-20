using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public float health;
    public TextMeshPro healthText;
    
    public float invTimer;
    public bool isInv;
    void Start()
    {
        health = 5;
    }
    
    void Update()
    {
        if (invTimer >= 0)
        {
            invTimer -= Time.deltaTime;
            isInv = true;
        }
        else
        {
            isInv = false;
        }
    }

///////// Collisions ///////////////
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Triangle Enemy"))
        {
            Enemy_Script hazard = other.GetComponent<Enemy_Script>();
            hazard.takeDamage(999);

            if (!isInv)
            {
                health--;
                healthChecker();
            }
        }
        
        else if (other.gameObject.CompareTag("Square Enemy"))
        {
            Enemy_Script shield = other.GetComponent<Enemy_Script>();
            shield.takeDamage(999);
        }
        
        else if (other.gameObject.CompareTag("Circle Enemy"))
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.takeDamage(999);
        }
        
        else if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            healthChecker();
        }
    }

    public void healthChecker()
    {
        healthText.text = "" + health;
        
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
    }

    //Invincibility frames for dashing
    public void setInvTimer(float num)
    {
        invTimer = num;
    }
}
