using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard_Script : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;

    public Vector2 velocity;
    //public Gun_Script Gun;
    void Start()
    {
        /*Gun = GameObject.FindWithTag("Gun").GetComponent<Gun_Script>();
        
        //Looks at that position
        var offset = -90f;
        Vector2 direction = Gun.getDirection();
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));*/
        
        Destroy(gameObject,3);
    }
    
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }
}
