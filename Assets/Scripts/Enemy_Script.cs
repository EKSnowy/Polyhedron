using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Script : MonoBehaviour
{
    // Code from: https://discussions.unity.com/t/how-do-you-make-enemies-rotate-to-your-position-in-2d/219214/3 //
    public Transform target;
    public float speed;

    public ParticleSystem particle;

    private void Start()
    {
        if (gameObject.tag == "Triangle Enemy")
        {
            speed = Random.Range(2, 5);
        }
        else if (gameObject.tag == "Square Enemy")
        {
            speed = Random.Range(1, 3);
        }
    }

    void Update()
    {
        //Moves towards player
        target = GameObject.FindWithTag("Player").transform;
        //If square enemy, only follows if far away from the player
        if (gameObject.tag == "Square Enemy")
        {
            if (Vector3.Distance(transform.position, target.position) > 3f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, 
                    speed * Time.deltaTime);
            }
        }
        //If triangle enemy, always follows player
        else if (gameObject.tag == "Triangle Enemy")
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 
                speed * Time.deltaTime);
        }
        
        //Looks at player
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
