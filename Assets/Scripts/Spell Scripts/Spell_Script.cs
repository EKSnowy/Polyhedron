using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float iceTimer;
    public float ballTimer;
    public float hypnosisTimer;
    public float shieldTimer;
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
    public GameObject Iceshards;
    public GameObject LightningDome;
    public GameObject BouncyBall;
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
    
    public Vector3 mousePos;
    void Update()
    {
        //Fire Toggle//
        if (toggleFire)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
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
            fireScript.setDamage(2);
        }
        
        else if (fireLevel == 2)
        {
            
        }
        
        else if (fireLevel == 3)
        {
            
        }
    }
    
    ///////////// Ice Spell /////////////////
    public void addIceLevel()
    {
        iceLevel++;

        if (iceLevel == 1)
        {
            toggleIce = true;
            iceScript.setDamage(1);
        }
        
        else if (iceLevel == 2)
        {
            
        }
        
        else if (iceLevel == 3)
        {
            
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
        }
        
        else if (lightningLevel == 2)
        {
            
        }
        
        else if (lightningLevel == 3)
        {
            
        }
    }
    
    ///////////// Ball Spell /////////////////
    public void addBallLevel()
    {
        ballLevel++;

        if (ballLevel == 1)
        {
            toggleBall = true;
            ballScript.setDamage(1);
        }
        
        else if (ballLevel == 2)
        {
            
        }
        
        else if (ballLevel == 3)
        {
            
        }
    }
    
    ///////////// Hypnosis Spell /////////////////
    public void addHypnosisLevel()
    {
        hypnosisLevel++;

        if (hypnosisLevel == 1)
        {
            toggleHypnosis = true;
        }
        
        else if (hypnosisLevel == 2)
        {
            
        }
        
        else if (hypnosisLevel == 3)
        {
            
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
