using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float maxHealth;
    
    [Header("Scripts")]
    public Audio_Manager AM;
    public HealthBar_Script HB;
    public Boss_Script bossField;

    [Header("Objects")]
    public GameObject particle;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        HB = GetComponentInChildren<HealthBar_Script>();
    }
    
    public void Death()
    {
        AM.playSound(1,.3f);
        //Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void takeDamage(float num)
    {
        health -= num;
        HB.UpdateHealthBar(health,maxHealth);

        if (health <= 0)
        {
            bossField.setImmune(false);
            Death();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            takeDamage(2);
        }
    }
}
