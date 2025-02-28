using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D RB;
    public Vector3 lastVelocity;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVelocity = RB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, 
            other.contacts[0].normal);
        RB.velocity = direction * Mathf.Max(speed, 0f);
    }
}
