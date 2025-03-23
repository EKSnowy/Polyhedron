using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ice_Script : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject iceShard;
    public IceShard_Script iceScript;
    
    public void setDamage(float num)
    {
        //iceScript.setDamage(num);
    }

    public void setSpeed(float num)
    {
        //iceScript.setSpeed(num);
    }

    public void Shoot()
    {
       Instantiate(iceShard, transform.position, transform.rotation);
    }

}
