using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bouncyball_Script : MonoBehaviour
{
    public float damage;
    public float speedMax;
    public float speedMin;
    
    public Rigidbody2D RB;
    public void setDamage(float num)
    {
        damage = num;
    }

    public void setSpeed(float min, float max)
    {
        speedMin = min;
        speedMax = max;
    }

    public void Fling()
    {
        RB.velocity = Vector2.zero;
        RB.AddForce(new Vector2(Random.Range(speedMin,speedMax),Random.Range(speedMin,speedMax)));
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
        }
        else if (other.tag == "Boss Shield")
        {
            Enemy_Shield shield = other.GetComponent<Enemy_Shield>();
            shield.takeDamage(damage);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy Shield")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),other.collider);
        }
        
        else if (other.gameObject.tag == "Ball")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),other.collider);
        }
    }
}
