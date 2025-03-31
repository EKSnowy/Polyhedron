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
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;
    public GameObject Option4;
    public GameObject Option5;
    public GameObject Option6;
    public int randomSpell1;
    public int randomSpell2;
    public int randomSpell3;
    [Header("Scripts")]
    public Spell_Script spellManager;
    public Enemy_Script enemyShieldScript;
    public Enemy_Script enemyHazardScript;
    public Enemy_Script enemyCircleScript;

    public Player_Script player;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        
        //Controls amount of enemies per wave
        enemyCount = 5;
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
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
        
        else if (canCheck)
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
        addWave();
        
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
        
        enemyHazardScript.addHealth(1.5f);
        enemyCircleScript.addHealth(1.5f);
        enemyShieldScript.addHealth(1.5f);
        
        //Waves System//
        if (waves % 2 == 0)
        {
            enemyCount += 2;
        }
    }
    
    /////////////// Shop ////////////////////

    public void startShop()
    {
        triggerShop = false;
        shopUI.SetActive(true);
        closeButton.SetActive(false);
        player.setInvTimer(60);

        randomSpell1 = Random.Range(1, 7);
        randomSpell2 = Random.Range(1, 7);
        randomSpell3 = Random.Range(1, 7);

        //Prevents options from repeating
        while (randomSpell2 == randomSpell1 || randomSpell2 == randomSpell3)
        {
            randomSpell2 = Random.Range(1, 7);
        }
        while (randomSpell3 == randomSpell1 || randomSpell3 == randomSpell2)
        {
            randomSpell3 = Random.Range(1, 7);
        }
        
        //Prevents options from overlapping with one another
        Option1.SetActive(false);
        Option2.SetActive(false);
        Option3.SetActive(false);
        Option4.SetActive(false);
        Option5.SetActive(false);
        Option6.SetActive(false);
        
        /////// Rerolls ////////
        
        //If a spell is maxed out, gives option to reroll
        if (spellManager.getMaxSpell())
        {
            rerollButton.SetActive(true);
            
            //If no more rerolls or maxed all spells, disables button and activates close shop
            if (rerollCounter <= 0 || spellManager.getReroll() == 6)
            {
                rerollButton.SetActive(false);
                closeButton.SetActive(true);
            }
        }
        else
        {
            rerollButton.SetActive(false);
        }
        
        ///// Slot 1 //////
        if (randomSpell1 == 1)
        {
            //Hypnosis
            if (!spellManager.getHypnosisMax())
            {
                Option1.transform.position = spot1.position;
                Option1.SetActive(true);
            }
        }
        else if (randomSpell1 == 2)
        {
            //Fireball
            if (!spellManager.getFireMax())
            {
                Option2.transform.position = spot1.position;
                Option2.SetActive(true);
            }
        }
        else if (randomSpell1 == 3)
        {
            //Ice Shards
            if (!spellManager.getIceMax())
            {
                Option3.transform.position = spot1.position;
                Option3.SetActive(true);
            }
        }
        else if (randomSpell1 == 4)
        {
            //Lightning
            if (!spellManager.getLightningMax())
            {
                Option4.transform.position = spot1.position;
                Option4.SetActive(true);
            }
        }
        else if (randomSpell1 == 5)
        {
            //Ball
            if (!spellManager.getBallMax())
            {
                Option5.transform.position = spot1.position;
                Option5.SetActive(true);
            }
        }
        else if (randomSpell1 == 6)
        {
            //Shield
            if (!spellManager.getShieldMax())
            {
                Option6.transform.position = spot1.position;
                Option6.SetActive(true);
            }
        }
        
        ///// Slot 2 //////
        if (randomSpell2 == 1)
        {
            //Hypnosis
            if (!spellManager.getHypnosisMax())
            {
                Option1.transform.position = spot2.position;
                Option1.SetActive(true);
            }
        }
        else if (randomSpell2 == 2)
        {
            //Fireball
            if (!spellManager.getFireMax())
            {
                Option2.transform.position = spot2.position;
                Option2.SetActive(true);
            }
        }
        else if (randomSpell2 == 3)
        {
            //Ice Shards
            if (!spellManager.getIceMax())
            {
                Option3.transform.position = spot2.position;
                Option3.SetActive(true);
            }
        }
        else if (randomSpell2 == 4)
        {
            //Lightning
            if (!spellManager.getLightningMax())
            {
                Option4.transform.position = spot2.position;
                Option4.SetActive(true);
            }
        }
        else if (randomSpell2 == 5)
        {
            //Ball
            if (!spellManager.getBallMax())
            {
                Option5.transform.position = spot2.position;
                Option5.SetActive(true);
            }
        }
        else if (randomSpell2 == 6)
        {
            //Shield
            if (!spellManager.getShieldMax())
            {
                Option6.transform.position = spot2.position;
                Option6.SetActive(true);
            }
        }
        
        ///// Slot 3 //////
        if (randomSpell3 == 1)
        {
            //Hypnosis
            if (!spellManager.getHypnosisMax())
            {
                Option1.transform.position = spot3.position;
                Option1.SetActive(true);
            }
        }
        else if (randomSpell3 == 2)
        {
            //Fireball
            if (!spellManager.getFireMax())
            {
                Option2.transform.position = spot3.position;
                Option2.SetActive(true);
            }
        }
        else if (randomSpell3 == 3)
        {
            //Ice Shards
            if (!spellManager.getIceMax())
            {
                Option3.transform.position = spot3.position;
                Option3.SetActive(true);
            }
        }
        else if (randomSpell3 == 4)
        {
            //Lightning
            if (!spellManager.getLightningMax())
            {
                Option4.transform.position = spot3.position;
                Option4.SetActive(true);
            }
        }
        else if (randomSpell3 == 5)
        {
            //Ball
            if (!spellManager.getBallMax())
            {
                Option5.transform.position = spot3.position;
                Option5.SetActive(true);
            }
        }
        else if (randomSpell3 == 6)
        {
            //Shield
            if (!spellManager.getShieldMax())
            {
                Option6.transform.position = spot3.position;
                Option6.SetActive(true);
            }
        }
    }
    
    
    /////////////// Spell Buttons ////////////////////
    
    public void spellHypnosis()
    {
        spellManager.addHypnosisLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
    }
    
    public void spellFireball()
    {
        spellManager.addFireLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
    }
    
    public void spellIceShards()
    {
        spellManager.addIceLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
    }
    
    public void spellLightningDome()
    {
        spellManager.addLightningLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
    }
    
    public void spellBouncyBall()
    {
        spellManager.addBallLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
    }

    public void spellShield()
    {
        spellManager.addShieldLevel();
        
        shopUI.SetActive(false);
        canSpawn = true;
        player.setInvTimer(0);
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
    }

    public void resetLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        SceneManager.LoadScene(sceneName);
    }
}
