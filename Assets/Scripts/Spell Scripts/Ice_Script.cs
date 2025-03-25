using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ice_Script : MonoBehaviour
{
    public GameObject iceShard;
    public IceShard_Script iceScript;
    public Transform shootingPos;
    
    public float bulletSpeed;
    public float bulletAmount;

    public void setBulletAmount(float amount)
    {
        bulletAmount = amount;
    }

    public void setDamage(float num)
    {
        iceScript.setDamage(num);
    }
    
    
    public void Shoot()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            var spawnedBullet = Instantiate(iceShard, shootingPos.position, shootingPos.rotation);
            spawnedBullet.transform.position = 
                new Vector3(shootingPos.position.x,shootingPos.position.y,-1f);
            
            Rigidbody2D bulletRB = spawnedBullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(transform.up * bulletSpeed);
            
            switch (i)
            {
                case 1:
                    spawnedBullet.transform.position = 
                        new Vector3(shootingPos.position.x + .8f,shootingPos.position.y + .8f,-1f);
                    break;
                
                case 2:
                    spawnedBullet.transform.position = 
                        new Vector3(shootingPos.position.x + -.8f,shootingPos.position.y + .8f,-1f);
                    break;
                
                case 3:
                    spawnedBullet.transform.position = 
                        new Vector3(shootingPos.position.x + 1.6f,shootingPos.position.y + -.5f,-1f);
                    break;
                
                case 4:
                    spawnedBullet.transform.position = 
                        new Vector3(shootingPos.position.x + -1.6f,shootingPos.position.y + -.5f,-1f);
                    break;
            }
        }
    }

}
