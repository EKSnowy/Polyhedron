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
    public float rotateSpeed;
    public float rotateModifier;

    public ParticleSystem particle;
    public Audio_Manager AM;

    public float health;
    public float maxHealth;
    
    public float maxHighHealth;
    public float minHighHealth;

    public float maxLowHealth;
    public float minLowHealth;
    
    public float healthModifier;
    
    public HealthBar_Script HB;

    public GameObject Fire;

    public bool inLightning;
    public bool isStunned;
    
    public SpriteRenderer SR;
    public float angle;
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        HB = GetComponentInChildren<HealthBar_Script>();
        
        //Stats are selected based on the enemy type detected through tags
        if (gameObject.tag == "Triangle Enemy")
        {
            speed = Random.Range(2, 5);
            maxHealth = Random.Range(maxLowHealth,minLowHealth);
            maxHealth += healthModifier;
            health = maxHealth;
        }
        else if (gameObject.tag == "Square Enemy")
        {
            speed = Random.Range(1, 3);
            maxHealth = Random.Range(maxHighHealth,minHighHealth);
            maxHealth += healthModifier;
            health = maxHealth;

            rotateSpeed = 10;
        }
        else if (gameObject.tag == "Circle Enemy")
        {
            maxHealth = Random.Range(maxLowHealth,minLowHealth);
            maxHealth += healthModifier;
            health = maxHealth;

            SR = GetComponent<SpriteRenderer>();
        }
        
    }

    public void setLowHealth(float min, float max)
    {
         minLowHealth = min;
         maxLowHealth = max;
    }
    
    public void setHighHealth(float min, float max)
    {
        minHighHealth = min;
        maxHighHealth = max;
    }

    public void addHealth(float num)
    {
        healthModifier += num;
    }

    public void resetHealth()
    {
        healthModifier = 0;
    }

    void Update()
    {
        //If not stunned, can move and rotate
        if (!isStunned)
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

            //Normal enemies rotate to look at player//
            if (gameObject.tag != "Square Enemy")
            {
                //Rotates to look at player
                var offset = 90f;
                Vector2 direction = target.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
            }
            else
            {
                /////////////////WORK IN PROGRESS///////////////
                
                //if a shield enemy, rotation lerps to the player
                Vector2 targetPos = target.position;
                Vector2 thisPos = transform.position;
                targetPos.x = targetPos.x - thisPos.x;
                targetPos.y = targetPos.y - thisPos.y;

                rotateSpeed = .004f;
                float tangle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 90f;
                angle = Mathf.Lerp(angle,tangle, rotateSpeed);
                transform.rotation = Quaternion.Euler(0,0,angle);
                //Debug.Log("angle" + angle);
                //Debug.Log("tangle" + tangle);
            }
        }
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

    //////Burn//////
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
    
    //////Lightning//////
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
    
    ///////Hypnosis///////
    public void startHypnosis(float duration)
    {
        StartCoroutine(Sleep(duration));
    }
    
    public IEnumerator Sleep(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public void changeSprite(Sprite sprite)
    {
        SR.sprite = sprite;
    }
}
