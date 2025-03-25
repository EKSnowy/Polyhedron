using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Bounce Code from: https://youtu.be/RoZG5RARGF0?si=YWrOxU8IgPAI_03O //
    
    private Rigidbody2D RB;
    
    public Vector3 lastVelocity;
    public Vector2 minVector;
    public Vector2 maxVector;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = RB.velocity;
        
        //////Checks if the force has hit the cap speed///////
        
        //If velocity is lower than min X
        if (lastVelocity.x <= minVector.x)
        {
            RB.velocity = new Vector2(minVector.x,RB.velocity.y);
        }
        //If velocity is lower than min Y
        else if (lastVelocity.y <= minVector.y)
        {
            RB.velocity = new Vector2(RB.velocity.x,minVector.y);
        }
        //If velocity is higher than max X
        else if (lastVelocity.x >= maxVector.x)
        {
            RB.velocity = new Vector2(maxVector.x,RB.velocity.y);
        }
        //If velocity is higher than max Y
        else if (lastVelocity.y >= maxVector.y)
        {
            RB.velocity = new Vector2(RB.velocity.x,maxVector.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Ignores the bouncy ball's collision
        if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Ball")
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),other.collider);
            }
        }
        //Bounces off walls
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, 
            other.GetContact(0).normal);
        RB.velocity = direction * Mathf.Max(speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Out of Bounds Box")
        {
            transform.position = new Vector3(0,0,transform.position.z);
        }
    }
}
