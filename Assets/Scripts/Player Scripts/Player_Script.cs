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
    
    static Gun_Script[] guns;
    void Start()
    {
        health = 5;
        guns = transform.GetComponentsInChildren<Gun_Script>();
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

        if (Input.GetMouseButtonDown(0))
        {
            foreach (Gun_Script gun in guns)
            {
                gun.Shoot();
            }
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
