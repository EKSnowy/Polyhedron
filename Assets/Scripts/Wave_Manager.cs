using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    public Audio_Manager AM;
    public List<GameObject> EnemyList;
    
    public float emptyCount;
    public float spawnTime;
    public float enemyCount;
    public bool canSpawn;
    public bool canCheck;

    public float waves;
    public TextMeshPro wavesText;
    public GameObject spawner;
    
    public GameObject shopUI;
    public bool triggerShop;
    //Spots for the spells
    public Transform spot1;
    public Transform spot2;
    public Transform spot3;
    //Spell options
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;
    public GameObject Option4;
    public GameObject Option5;
    public GameObject Option6;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        
        //Controls amount of enemies per wave
        enemyCount = 5;
        canSpawn = true;
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
        
        else if (triggerShop)
        {
            startShop();
        }
    }
    
    public void checkList()
    {
        emptyCount = 0;
        
        for(int i = 0; i < EnemyList.Count; i++)
        {
            //Counts for each enemy missing in list
            if (EnemyList[i] == null)
            {
                emptyCount++;
            }
            
            ///if the entire enemy list is empty, triggers shop and moves to next wave
            if (emptyCount == EnemyList.Count)
            {
                triggerShop = true;
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

    public void addWave()
    {
        waves++;
        wavesText.text = "Wave: " + waves;
    }
    
    /////////////// Shop ////////////////////

    public void startShop()
    {
        Time.timeScale = 0;
        shopUI.SetActive(true);

        int randomSpell1 = Random.Range(1, 7);
        int randomSpell2 = Random.Range(1, 7);
        int randomSpell3 = Random.Range(1, 7);
        
        
    }
    
    
    /////////////// Spells ////////////////////
    
    public void spellHypnosis()
    {
        Debug.Log("Hypnosis Chosen");
        
        
    }
    
    public void spellFireball()
    {
        Debug.Log("Fireball Chosen");
    }
    
    public void spellIceShards()
    {
        Debug.Log("Ice shards Chosen");
    }
    
    public void spellLightningDome()
    {
        Debug.Log("lightning dome Chosen");
    }
    
    public void spellBouncyBall()
    {
        Debug.Log("bouncy ball Chosen");
    }

    public void spellShield()
    {
        Debug.Log("Shield Chosen");
    }
}
