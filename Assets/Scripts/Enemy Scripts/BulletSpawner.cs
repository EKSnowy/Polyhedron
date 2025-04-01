using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // From: https://youtu.be/YNJM7rWbbxY?si=xKeOcb88RfijjS3s //
    enum SpawnerType { Straight, Spin }
    
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float bulletSpeed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    public float rotationSpeed;


    private GameObject spawnedBullet;
    private float timer = 0f;
   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f,0f,transform.eulerAngles.z + rotationSpeed);
        if(timer >= firingRate) {
            Fire();
            timer = 0;
        }
    }
    
    private void Fire() {
        if(bullet) {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Boss_Projectile>().speed = bulletSpeed;
            spawnedBullet.GetComponent<Boss_Projectile>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    public void bulletPattern(string type)
    {
        if (type == "slow spin")
        {
            bulletLife = 5;
            bulletSpeed = 2;
            firingRate = .5f;
            rotationSpeed = 0;
        }
        
        else if (type == "wild spin")
        {
            bulletLife = 10;
            bulletSpeed = 1;
            firingRate = .5f;
            rotationSpeed = 5;
        }
        
        else if (type == "spread")
        {
            bulletLife = 10;
            bulletSpeed = 1;
            firingRate = .2f;
            rotationSpeed = 10;
        }
    }
}