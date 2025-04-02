using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shield : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float maxHealth;
    public bool inLightning;
    
    [Header("Scripts")]
    public Audio_Manager AM;
    public HealthBar_Script HB;
    public Boss_Script bossField;

    [Header("Objects")]
    public GameObject particle;
    public GameObject Fire;
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
    ////////////////Burn/////////////////
    public void startBurn(float amount, float damage)
    {
        StartCoroutine(Burn(amount, damage));
    }
    
    public IEnumerator Burn(float amount, float damage)
    {
        float burnCounter = amount;
        
        while (burnCounter > 0)
        {
            Fire.SetActive(true);
            yield return new WaitForSeconds(1);
            takeDamage(damage);
            burnCounter--;
        }
        
        Fire.SetActive(false);
    }
    ////////////Lightning/////////////
    public void startLightning(float time, float damage)
    {
        inLightning = true;
        StartCoroutine(takeLightningDamage(time, damage));
    }
    
    public void stopLightning()
    {
        inLightning = false;
        StopCoroutine(takeLightningDamage(0,0));
    }
    
    public IEnumerator takeLightningDamage(float time, float damage)
    {
        while (inLightning)
        {
            takeDamage(damage);
            yield return new WaitForSeconds(time);
        }
    }
}
