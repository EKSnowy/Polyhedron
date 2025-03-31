using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave_Manager : MonoBehaviour
{
    public Audio_Manager AM;
    public List<GameObject> EnemyList;
    [Header("Enemy Check / Spawner")]
    public float spawnTime;
    public float enemyCount;
    public bool canSpawn;
    public bool canCheck;
    [Header("Waves")]
    public float waves;
    public bool isEndless;
    public bool bossLevel;
    public float difficultyMod;
    public TextMeshPro wavesText;
    public GameObject spawner;
    [Header("Shop")]
    public GameObject shopUI;
    public bool triggerShop;
    public float shopCounter;
    [Header("Shop Buttons")]
    public GameObject closeButton;
    public GameObject rerollButton;
    public float rerollCounter;
    [Header("Spell Slots")]
    public Transform spot1;
    public Transform spot2;
    public Transform spot3;
    [Header("Spell Options")] 
    public List<GameObject> spellList;
    public int randomSpell1;
    public int randomSpell2;
    public int randomSpell3;
    [Header("Scripts")]
    public Spell_Script spellManager;
    public Enemy_Script enemyShieldScript;
    public Enemy_Script enemyHazardScript;
    public Enemy_Script enemyCircleScript;
    public Level_Randomizer randomizer;
    public Music_Script musicScript;

    public Player_Script player;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        
        //Controls amount of enemies per wave
        enemyCount = 5;
        difficultyMod = 1.5f;
        canSpawn = true;
        
        enemyHazardScript.resetHealth();
        enemyCircleScript.resetHealth();
        enemyShieldScript.resetHealth();
        
        enemyHazardScript.setLowHealth(2,4);
        enemyCircleScript.setLowHealth(2,4);
        enemyShieldScript.setHighHealth(5,6);
    }
    
    void Update()
    {
        if (!bossLevel)
        {
            if (canSpawn)
            {
                StartCoroutine(Spawn());
            }
        }
        
        if (canCheck)
        {
            checkList();
        }
        
        if (triggerShop)
        {
            startShop();
            shopCounter++;

            if (shopCounter % 3 == 0)
            {
                player.addHealth(1);
            }
        }
    }
    
    public void checkList()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            //Removes dead enemies from the list
            if (EnemyList[i] == null)
            {
                EnemyList.Remove(EnemyList[i]);
            }
            
            ///if the entire enemy list is empty, triggers shop and moves to next wave
            if (EnemyList.Count == 0)
            {
                rerollCounter = spellManager.getReroll();
                
                triggerShop = true;
                canCheck = false;
            }
        }
    }
    ///Adds a wave to the counter and randomly spawns enemy spawners at slightly random intervals
    IEnumerator Spawn()
    {
        canSpawn = false;
        canCheck = false;
        
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            float randomY = Random.Range(-4f,4f);

            Instantiate(spawner, new Vector2(randomX, randomY), Quaternion.identity);
            spawnTime = Random.Range(1f,.5f);
            
            yield return new WaitForSeconds(spawnTime);
        }

        canCheck = true;
    }
    //Adds the enemies spawned by the spawner to the list
    public void addList(GameObject obj)
    {
        EnemyList.Add(obj);
    }

    ////////////////////Waves////////////////////////
    public void addWave()
    {
        waves++;
        wavesText.text = "Wave:" + waves;
        
        enemyHazardScript.addHealth(difficultyMod);
        enemyCircleScript.addHealth(difficultyMod);
        enemyShieldScript.addHealth(difficultyMod);
        
        ////////Waves System///////
        //Every 2 waves, increases count by 2//
        if (waves % 2 == 0)
        {
            enemyCount += 2;
        }
        //If on boss stage, play boss music and set up boss level//
        if (waves == 10)
        {
            bossLevel = true;
            randomizer.bossLevelRandomize();
            musicScript.playBossSong();
        }
        //If on endless, change level every 10 waves//
        if (waves % 10 == 0 && isEndless)
        {
            randomizer.levelRandomize();
        }
    }
    
    /////////////// Shop ////////////////////

    public void startShop()
    {
        triggerShop = false;
        shopUI.SetActive(true);
        closeButton.SetActive(false);
        player.setInvTimer(60);

        //If the spell option is maxed, removes from list//
        for (int i = 0; i < spellList.Count; i++)
        {
            SpellOption_Script spellScript = spellList[i].GetComponent<SpellOption_Script>();
            
            if (spellScript.getMax())
            {
                spellList[i].SetActive(false);
                spellList.Remove(spellList[i]);
                i--;
            }
        }
        
        randomSpell1 = Random.Range(0, spellList.Count);
        randomSpell2 = Random.Range(0, spellList.Count);
        randomSpell3 = Random.Range(0, spellList.Count);

        //Prevents options from repeating if more than 2 options
        if (spellList.Count > 2)
        {
            while (randomSpell2 == randomSpell1 || randomSpell2 == randomSpell3)
            {
                randomSpell2 = Random.Range(0, spellList.Count);
            }
            while (randomSpell3 == randomSpell1 || randomSpell3 == randomSpell2)
            {
                randomSpell3 = Random.Range(0, spellList.Count);
            }
        }
        //Prevents options from overlapping with one another
        foreach (GameObject spell in spellList)
        {
            spell.SetActive(false);
        }
        
        /////// Rerolls ////////
        
        //If a spell is maxed out, gives option to reroll
        if (spellManager.getMaxSpell())
        {
            rerollButton.SetActive(true);
            
            //If no more rerolls or maxed all spells, disables button and activates close shop
            if (rerollCounter <= 0 || spellList.Count <= 0)
            {
                closeButton.SetActive(true);
                rerollButton.SetActive(false);
            }
        }
        else
        {
            rerollButton.SetActive(false);
        }

        //If there are spells left, randomizes which goes in each slot
        if (spellList.Count > 0)
        {
            ///// Slot 1 //////
            spellList[randomSpell1].transform.position = spot1.position;
            spellList[randomSpell1].SetActive(true);
       
            ///// Slot 2 //////
            spellList[randomSpell2].transform.position = spot2.position;
            spellList[randomSpell2].SetActive(true);
        
            ///// Slot 3 //////
            spellList[randomSpell3].transform.position = spot3.position;
            spellList[randomSpell3].SetActive(true);
        }
    }
    
    /////////////// Spell Buttons ////////////////////
    
    public void spellHypnosis()
    {
        spellManager.addHypnosisLevel();
        
        closeShop();
    }
    
    public void spellFireball()
    {
        spellManager.addFireLevel();
        
        closeShop();
    }
    
    public void spellIceShards()
    {
        spellManager.addIceLevel();
        
        closeShop();
    }
    
    public void spellLightningDome()
    {
        spellManager.addLightningLevel();
        
        closeShop();
    }
    
    public void spellBouncyBall()
    {
        spellManager.addBallLevel();
        
        closeShop();
    }

    public void spellShield()
    {
        spellManager.addShieldLevel();
        
        closeShop();
    }

    public void Reroll()
    {
        if (rerollCounter > 0)
        {
            rerollCounter--;
            startShop();
        }
    }

    public void closeShop()
    {
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
        
        addWave();
    }

    public void resetLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        SceneManager.LoadScene(sceneName);
    }

    /////////////////Endless & Boss////////////////////
    
    public void toggleEndless()
    {
        isEndless = true;
        musicScript.randomizeSong();
        randomizer.levelRandomize();
        difficultyMod = 2f;
    }
    
    //public 
    
    //////////Extra///////////
    
    public List<GameObject> getList()
    {
        return spellList;
    }
}
