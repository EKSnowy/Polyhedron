using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spell_Script : MonoBehaviour
{
    //Spell levels
    public float fireLevel;
    public float iceLevel;
    public float lightningLevel;
    public float ballLevel;
    public float hypnosisLevel;
    public float shieldLevel;

    public float rerollLevel;
    //Spell timers
    public float fireTimer;
    public float maxFireTimer;
    
    public float iceTimer;
    public float maxIceTimer;
    
    public float ballTimer;
    public float maxBallTimer;
    
    public float hypnosisTimer;
    public float maxHypnosisTimer;
    //Spell booleans
    public bool toggleFire;
    public bool toggleIce;
    public bool toggleLightning;
    public bool toggleBall;
    public bool toggleHypnosis;
    public bool toggleShield;

    public bool maxFire;
    public bool maxIce;
    public bool maxLightning;
    public bool maxBall;
    public bool maxHypnosis;
    public bool maxShield;

    public bool maxSpell;
    //Spell damage
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;
    public float ballDamage;
    public float hypnosisDamage;
    public float shieldHealth;
    //Spell Objects
    public GameObject Fireball;
    public GameObject LightningDome;
    public GameObject BouncyBall;
    public List<GameObject> BallList;
    public GameObject Hypnosis;
    
    public GameObject shield1;
    public GameObject shield2;
    public GameObject shield3;
    public GameObject shield4;
    
    //Spell Scripts
    public Fireball_Script fireScript;
    public Ice_Script iceScript;
    public Lightning_Script lightningScript;
    public Bouncyball_Script ballScript;
    public Hypnosis_Script hypnosisScript;
    public Shield_Script shieldScript1;
    public Shield_Script shieldScript2;
    public Shield_Script shieldScript3;
    public Shield_Script shieldScript4;
    
    public Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        //Since Sprite is separate from shield, it turns off the sprites at the start
        shieldScript1.disableSprite();
        shieldScript2.disableSprite();
        shieldScript3.disableSprite();
        shieldScript4.disableSprite();
    }

    void Update()
    {
        //Fire Toggle//
        if (toggleFire)
        {
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
            //If cooldown is up
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Instantiate(Fireball, player.position, Quaternion.identity);
                    fireTimer = maxFireTimer;
                }
            }
        }
        
        //Ice Toggle//
        if (toggleIce)
        {
            if (iceTimer > 0)
            {
                iceTimer -= Time.deltaTime;
            }
            //If cooldown up
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    iceScript.Shoot();
                    iceTimer = maxIceTimer;
                }
            }
        }

        //Lightning Toggle//
        if (toggleLightning)
        {
            LightningDome.SetActive(true);
        }
        
        //Ball Toggle//
        if (toggleBall)
        {
            BouncyBall.SetActive(true);
            
            if (ballTimer > 0)
            {
                ballTimer -= Time.deltaTime;
            }
            else
            {
                foreach (GameObject ball in BallList)
                {
                    Bouncyball_Script script = ball.GetComponent<Bouncyball_Script>();
                    script.Fling();
                }
                ballTimer = maxBallTimer;
            }
        }

        //Hypnosis toggle//
        if (toggleHypnosis)
        {
            if (hypnosisTimer > 0)
            {
                hypnosisTimer -= Time.deltaTime;
            }
            //If cooldown is up
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Instantiate(Hypnosis, player.position, Quaternion.identity);
                    hypnosisTimer = maxHypnosisTimer;
                }
            }
        }
    }
///////////// Fire Spell /////////////////
    public void addFireLevel()
    {
        fireLevel++;

        if (fireLevel == 1)
        {
            toggleFire = true;
            fireScript.setDamage(3);
            fireScript.setBurn(2,1);
            maxFireTimer = 3;
        }
        
        else if (fireLevel == 2)
        {
            fireScript.setDamage(5);
            fireScript.setBurn(3,2);
            maxFireTimer = 2.5f;
        }
        
        else if (fireLevel == 3)
        {
            fireScript.setDamage(8);
            fireScript.setBurn(5,2);
            maxFireTimer = 2;
            
            //Maxed level
            maxFire = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////////// Ice Spell /////////////////
    public void addIceLevel()
    {
        iceLevel++;

        if (iceLevel == 1)
        {
            toggleIce = true;
            maxIceTimer = 3f;
            iceScript.setBulletAmount(1);
            iceScript.setDamage(2);
        }
        
        else if (iceLevel == 2)
        {
            iceScript.setBulletAmount(2);
            iceScript.setDamage(3);
        }
        
        else if (iceLevel == 3)
        {
            maxIceTimer = 2f;
            iceScript.setBulletAmount(3);
            iceScript.setDamage(4);
        }
        
        else if (iceLevel == 4)
        {
            maxIceTimer = 2f;
            iceScript.setBulletAmount(4);
            iceScript.setDamage(5);
        }
        
        else if (iceLevel == 5)
        {
            maxIceTimer = 1f;
            iceScript.setBulletAmount(5);
            iceScript.setDamage(6);
            
            //Maxed level
            maxIce = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////////// Lightning Spell /////////////////
    public void addLightningLevel()
    {
        lightningLevel++;

        if (lightningLevel == 1)
        {
            toggleLightning = true;
            lightningScript.setDamage(1);
            lightningScript.setTime(1);
            lightningScript.setSize(2.5f,2.5f);
        }
        
        else if (lightningLevel == 2)
        {
            lightningScript.setDamage(1.5f);
            lightningScript.setTime(.9f);
            lightningScript.setSize(3f,3f);
        }
        
        else if (lightningLevel == 3)
        {
            lightningScript.setDamage(2);
            lightningScript.setTime(.8f);
            lightningScript.setSize(3.5f,3.5f);
        }
        
        else if (lightningLevel == 4)
        {
            lightningScript.setDamage(2.5f);
            lightningScript.setTime(.7f);
            lightningScript.setSize(4f,4f);
        }
        
        else if (lightningLevel == 5)
        {
            lightningScript.setDamage(3);
            lightningScript.setTime(.6f);
            lightningScript.setSize(4.5f,4.5f);
            
            //Maxed level
            maxLightning = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////////// Ball Spell /////////////////
    public void addBallLevel()
    {
        ballLevel++;

        if (ballLevel == 1)
        {
            toggleBall = true;
            
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            
            BallList.Add(Instantiate(BouncyBall,new Vector3(randomX,randomY),
                Quaternion.identity));
            
            ballScript.setDamage(2);
            ballScript.setSpeed(.1f,.15f);
            maxBallTimer = 20;
        }
        
        else if (ballLevel == 2)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            
            BallList.Add(Instantiate(BouncyBall,new Vector3(randomX,randomY),
                Quaternion.identity));

            ballTimer = 0;

            foreach (GameObject ball in BallList)
            {
                Bouncyball_Script script = ball.GetComponent<Bouncyball_Script>();
                script.setDamage(3);
            }
        }
        
        else if (ballLevel == 3)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            
            BallList.Add(Instantiate(BouncyBall,new Vector3(randomX,randomY),
                Quaternion.identity));

            ballTimer = 0;
            maxBallTimer = 15;
            
            foreach (GameObject ball in BallList)
            {
                Bouncyball_Script script = ball.GetComponent<Bouncyball_Script>();
                script.setDamage(4);
            }
        }
        
        else if (ballLevel == 4)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            
            BallList.Add(Instantiate(BouncyBall,new Vector3(randomX,randomY),
                Quaternion.identity));

            ballTimer = 0;
            
            foreach (GameObject ball in BallList)
            {
                Bouncyball_Script script = ball.GetComponent<Bouncyball_Script>();
                script.setDamage(5);
            }
        }
        
        else if (ballLevel == 5)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);
            
            BallList.Add(Instantiate(BouncyBall,new Vector3(randomX,randomY),
                Quaternion.identity));

            ballTimer = 0;
            maxBallTimer = 10;
            
            foreach (GameObject ball in BallList)
            {
                Bouncyball_Script script = ball.GetComponent<Bouncyball_Script>();
                script.setDamage(6);
            }
            
            //Maxed level
            maxBall = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////////// Hypnosis Spell /////////////////
    public void addHypnosisLevel()
    {
        hypnosisLevel++;

        if (hypnosisLevel == 1)
        {
            toggleHypnosis = true;
            
            hypnosisScript.setDamage(0);
            hypnosisScript.setSize(.6f,.6f);
            hypnosisScript.setDuration(.5f);
            maxHypnosisTimer = 5;
        }
        
        else if (hypnosisLevel == 2)
        {
            hypnosisScript.setDamage(1);
            hypnosisScript.setSize(.65f,.65f);
            hypnosisScript.setDuration(.6f);
            maxHypnosisTimer = 4;
        }
        
        else if (hypnosisLevel == 3)
        {
            hypnosisScript.setDamage(2);
            hypnosisScript.setSize(.7f,.7f);
            hypnosisScript.setDuration(.7f);
            maxHypnosisTimer = 3;
        }
        
        else if (hypnosisLevel == 4)
        {
            hypnosisScript.setDamage(3);
            hypnosisScript.setSize(.75f,.75f);
            hypnosisScript.setDuration(.8f);
            maxHypnosisTimer = 2;
        }
        
        else if (hypnosisLevel == 5)
        {
            hypnosisScript.setDamage(4);
            hypnosisScript.setSize(.8f,.8f);
            hypnosisScript.setDuration(.9f);
            maxHypnosisTimer = 1.5f;
            
            //Maxed level
            maxHypnosis = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////////// Shield Spell /////////////////
    public void addShieldLevel()
    {
        shieldLevel++;

        if (shieldLevel == 1)
        {
            shield1.SetActive(true);
            shieldScript1.setHealth(1);
        }
        
        else if (shieldLevel == 2)
        {
            shield2.SetActive(true);
            shieldScript2.setHealth(1);
        }
        
        else if (shieldLevel == 3)
        {
            shield3.SetActive(true);
            shieldScript3.setHealth(1);
        }
        
        else if (shieldLevel == 4)
        {
            shield4.SetActive(true);
            shieldScript4.setHealth(1);
        }
        
        else if (shieldLevel == 5)
        {
            shieldScript1.setHealth(2);
            shieldScript2.setHealth(2);
            shieldScript3.setHealth(2);
            shieldScript4.setHealth(2);

            //Maxed Level
            maxShield = true;
            maxSpell = true;
            rerollLevel++;
        }
    }
    
    ///////// Max Spell Checker ///////////

    public bool getFireMax()
    {
        return maxFire;
    }
    public bool getIceMax()
    {
        return maxIce;
    }
    public bool getLightningMax()
    {
        return maxLightning;
    }
    public bool getBallMax()
    {
        return maxBall;
    }
    public bool getHypnosisMax()
    {
        return maxHypnosis;
    }
    public bool getShieldMax()
    {
        return maxShield;
    }

    //If any spells are maxed, this returns true
    public bool getMaxSpell()
    {
        return maxSpell;
    }

    //For every spell maxed, adds one to reroll counter
    public float getReroll()
    {
        return rerollLevel;
    }
}
