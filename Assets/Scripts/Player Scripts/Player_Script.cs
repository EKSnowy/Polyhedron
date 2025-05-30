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

    public GameObject deathScreen;
    public bool isDead;
    
    public SpriteRenderer SR;
    public Color normal;
    public Color transparent;
    void Start()
    {
        normal = SR.color;
        health = 5;
    }
    
    void Update()
    {
        if (invTimer >= 0)
        {
            invTimer -= Time.deltaTime;
            SR.color = transparent;
        }
        else
        {
            isInv = false;
            SR.color = normal;
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
            if (!isInv)
            {
                health--;
                healthChecker();
            }
        }
        
        else if (other.gameObject.CompareTag("Boss Bullet"))
        {
            Boss_Projectile bossBullet = other.GetComponent<Boss_Projectile>();
            
            if (!isInv)
            {
                health--;
                healthChecker();
                bossBullet.Death();
            }
        }
    }
    
    public void addHealth(float num)
    {
        health += num;
        healthChecker();
    }

    public void healthChecker()
    {
        healthText.text = "" + health;
        
        if (health <= 0)
        {
            deathScreen.SetActive(true);
            isDead = true;
            Time.timeScale = 0;
        }
    }

    //Invincibility frames for dashing
    public void setInvTimer(float num)
    {
        isInv = true;
        invTimer = num;
    }

    public float getHealth()
    {
        return health;
    }
}
