using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float maxHealth;
    public bool isImmune;
    
    [Header("Scripts")]
    public Audio_Manager AM;
    public HealthBar_Script HB;
    public Boss_Script coreScript;

    [Header("Objects")]
    public GameObject particle;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        HB = GetComponentInChildren<HealthBar_Script>();
        
        //The core is immune until barrier is broken
        coreScript.setImmune(true);
        isImmune = true;
    }
    
    public void Death()
    {
        AM.playSound(1,.3f);
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void takeDamage(float num)
    {
        health -= num;
        HB.UpdateHealthBar(health,maxHealth);

        if (health <= 0)
        {
            coreScript.setImmune(false);
            Death();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (!isImmune)
            {
                takeDamage(5);
            }
        }
    }

    public void setImmune(bool b)
    {
        isImmune = b;
    }
}
