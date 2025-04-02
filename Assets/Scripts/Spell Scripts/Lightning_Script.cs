using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Script : MonoBehaviour
{
    public float damage;
    public float damageTime;
    
    public void setDamage(float num)
    {
        damage = num;
    }

    public void setTime(float num)
    {
        damageTime = num;
    }

    public void setSize(float sizeX, float sizeY)
    {
        transform.localScale = new Vector2(sizeX,sizeY);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Circle Enemy")
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.startLightning(damageTime,damage);
        }
        else if (other.tag == "Square Enemy")
        {
            Enemy_Script square = other.GetComponent<Enemy_Script>();
            square.startLightning(damageTime,damage);
        }
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.startLightning(damageTime,damage);
        }
        else if (other.tag == "Boss")
        {
            Boss_Script boss = other.GetComponent<Boss_Script>();
            boss.startLightning(damageTime,damage);
            boss.takeDamage(damage);
        }
        else if (other.tag == "Boss Shield")
        {
            Enemy_Shield shield = other.GetComponent<Enemy_Shield>();
            shield.startLightning(damageTime,damage);
            shield.takeDamage(damage);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Circle Enemy")
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.stopLightning();
        }
        else if (other.tag == "Square Enemy")
        {
            Enemy_Script square = other.GetComponent<Enemy_Script>();
            square.stopLightning();
        }
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.stopLightning();
        }
        else if (other.tag == "Boss")
        {
            Boss_Script boss = other.GetComponent<Boss_Script>();
            boss.stopLightning();
        }
        else if (other.tag == "Boss Shield")
        {
            Enemy_Shield shield = other.GetComponent<Enemy_Shield>();
            shield.stopLightning();
        }
    }
}
