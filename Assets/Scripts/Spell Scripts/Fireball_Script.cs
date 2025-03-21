using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball_Script : MonoBehaviour
{
    public float damage;
    public float burnTime;
    public float burnDamage;
    
    // Some help from: https://youtu.be/--u20SaCCow?si=wp2xjS2IYcgveu6T //
    public float speed;
    public Rigidbody2D RB;
    
    public ParticleSystem particle;
    void Start()
    {
        //Finds target location when spawned in
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RB = GetComponent<Rigidbody2D>();
        
        //Looks at that position
        var offset = 90f;
        Vector2 direction = mousePos - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        RB.velocity = new Vector2(direction.x,direction.y) * speed;
    }
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Death();
        }

        else if (other.tag == "Circle Enemy")
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.takeDamage(damage);
            circle.startBurn(burnTime, burnDamage);
            Death();
        }
        else if (other.tag == "Square Enemy")
        {
            Enemy_Script square = other.GetComponent<Enemy_Script>();
            square.takeDamage(damage);
            square.startBurn(burnTime,burnDamage);
            Death();
        }
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.takeDamage(damage);
            triangle.startBurn(burnTime, burnDamage);
            Death();
        }
    }

    public void setDamage(float num)
    {
        damage = num;
    }

    public void setBurn(float time, float damage)
    {
        burnTime = time;
        burnDamage = damage;
    }
    
    public void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
