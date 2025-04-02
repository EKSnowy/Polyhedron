using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss_Script : MonoBehaviour
{
    [Header("Values")]
    public float health;
    public float maxHealth;
    public bool isImmune;
    public bool inLightning;

    public int slowSpinTime;
    public int wildSpinTime;
    public int spreadTime;
    public int randomPhase;
    public int randomRotation;

    [Header("Scripts")] 
    public Wave_Manager WM;
    public Audio_Manager AM;
    public HealthBar_Script HB;
    public Boss_Script coreScript;
    public BulletSpawner bulletSpawner;
    public Screen_Shake screenShake;

    [Header("Objects")]
    public GameObject particle;
    public GameObject boss;
    public GameObject Fire;
    public GameObject bossText;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        WM = GameObject.FindWithTag("Game Manager").GetComponent<Wave_Manager>();
        HB = GetComponentInChildren<HealthBar_Script>();
        screenShake = GameObject.FindWithTag("MainCamera").GetComponent<Screen_Shake>();
        
        //The core is immune until barrier is broken
        coreScript.setImmune(true);
        isImmune = true;

        Instantiate(bossText, new Vector2(-5.48244381f, 1.12619996f), Quaternion.identity);
        StartCoroutine(bossPhases());
    }
    
    public void Death()
    {
        AM.playSound(1,.3f);
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void takeDamage(float num)
    {
        if (!isImmune)
        {
            health -= num;
            HB.UpdateHealthBar(health,maxHealth);
        }

        //If the field dies, the core can take damage//
        if (health <= 0 && gameObject.tag != "Boss")
        {
            coreScript.setImmune(false);
            Death();
        }
        //If core is destroyed, remove entire boss//
        else if (health <= 0)
        {
            AM.playSound(1,.3f);
            Instantiate(particle, transform.position, Quaternion.identity);
            screenShake.setShakeTime(.2f);
            WM.toggleEndless();
            Destroy(boss);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (!isImmune)
            {
                takeDamage(2);
            }
        }
    }

    public void setImmune(bool b)
    {
        isImmune = b;
    }

    public IEnumerator bossPhases()
    {
        randomizeTime();

        while (coreScript.getHealth() > 0)
        {
            //If slow spin//
            //Also spawns enemies//
            if (randomPhase == 1)
            {
                bulletSpawner.bulletPattern("slow spin");
                WM.startSpawn();
                
                if (randomRotation == 4)
                {
                    bulletSpawner.switchRotation();
                }
                yield return new WaitForSeconds(slowSpinTime);
                randomizeTime();
            }
            //If wild spin//
            else if (randomPhase == 2)
            {
                bulletSpawner.bulletPattern("wild spin");
                
                if (randomRotation == 4)
                {
                    bulletSpawner.switchRotation();
                }
                yield return new WaitForSeconds(wildSpinTime);
                randomizeTime();
            }
            //If spread//
            else if (randomPhase == 3)
            {
                bulletSpawner.bulletPattern("spread");
                
                if (randomRotation == 4)
                {
                    bulletSpawner.switchRotation();
                }
                yield return new WaitForSeconds(spreadTime);
                randomizeTime();
            }
        }
    }

    public void randomizeTime()
    {
        slowSpinTime = Random.Range(20, 30);
        wildSpinTime = Random.Range(6, 12);
        spreadTime = Random.Range(9, 12);

        randomPhase = Random.Range(1, 4);
        randomRotation = Random.Range(1, 5);
    }

    public float getHealth()
    {
        return health;
    }
    
    //////////////Burn//////////////
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
