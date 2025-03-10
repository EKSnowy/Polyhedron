using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    public Audio_Manager AM;
    public List<GameObject> EnemyList;
    
    public float emptyCount;
    public float spawnTime;
    public float spawnCooldown;
    public float enemyCount;
    public bool canSpawn;
    public bool canCheck;

    public GameObject spawner;
    void Start()
    {
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        
        spawnCooldown = 2;
        enemyCount = 5;
        canSpawn = true;
    }

    // Update is called once per frame
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
    }

    public void checkList()
    {
        emptyCount = 0;
        
        for(int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i] == null)
            {
                emptyCount++;
            }
            
            if (emptyCount == EnemyList.Count)
            {
                canSpawn = true;
            }
        }
    }

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

    public void addList(GameObject obj)
    {
        EnemyList.Add(obj);
    }
}
