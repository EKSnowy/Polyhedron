using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletCooldown;

    public Audio_Manager AM;

    public Sprite CircleEnemy;
    public Sprite CircleWarning;

    public Enemy_Script enemy;
    private void Start()
    {
        bulletCooldown = Random.Range(1, 4);
        
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
    }

    void Update()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.deltaTime;
        }
        else
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            AM.playSound(0,1);
            bulletCooldown = Random.Range(1, 4);
            enemy.changeSprite(CircleEnemy);
        }

        if (bulletCooldown <= .5f)
        {
            enemy.changeSprite(CircleWarning);
        }
    }
}
