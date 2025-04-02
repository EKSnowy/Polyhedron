using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard_Script : MonoBehaviour
{
    public float damage;
    public ParticleSystem particle;
    
    void Start()
    {
        Destroy(gameObject,2);
    }

    public void setDamage(float num)
    {
        damage = num;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Circle Enemy")
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.takeDamage(damage);
        }
        else if (other.tag == "Square Enemy")
        {
            Enemy_Script square = other.GetComponent<Enemy_Script>();
            square.takeDamage(damage);
        }
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.takeDamage(damage);
        }
        else if (other.tag == "Boss")
        {
            Boss_Script boss = other.GetComponent<Boss_Script>();
            boss.takeDamage(damage);
            Death();
        }
        else if (other.tag == "Boss Shield")
        {
            Enemy_Shield shield = other.GetComponent<Enemy_Shield>();
            shield.takeDamage(damage);
            Death();
        }
    }

    public void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
