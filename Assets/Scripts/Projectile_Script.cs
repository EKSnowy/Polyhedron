using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour
{
    // Some help from: https://youtu.be/--u20SaCCow?si=wp2xjS2IYcgveu6T //
    public float speed;
    public Transform target;
    public Rigidbody2D RB;
    
    public ParticleSystem particle;
    void Start()
    {
        //Finds target location when spawned in
        target = GameObject.FindWithTag("Player").transform;
        RB = GetComponent<Rigidbody2D>();
        
        //Looks at that position
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        RB.velocity = new Vector2(direction.x,direction.y) * speed;
    }
    
    public void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Circle Enemy")
        {
            if (other.tag == "Lightning")
            {
                //Does this so the projectile goes through the lightning (won't otherwise)
                Debug.Log("Hit lightning");
            }
            else
            {
                Death();
            }
        }
    }
}
