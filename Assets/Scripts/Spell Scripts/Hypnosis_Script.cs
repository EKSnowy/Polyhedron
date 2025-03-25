using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypnosis_Script : MonoBehaviour
{
    public float damage;
    public float duration;
    private void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = mousePos;
    }

    void Update()
    {
        Destroy(gameObject,duration);
    }

    public void setDamage(float num)
    {
        damage = num;
    }

    public void setDuration(float num)
    {
        duration = num;
    }
    
    public void setSize(float sizeX, float sizeY)
    {
        transform.localScale = new Vector2(sizeX,sizeY);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Circle Enemy")
        {
            Enemy_Script circle = other.GetComponent<Enemy_Script>();
            circle.startHypnosis(duration);
        }
        else if (other.tag == "Square Enemy")
        {
            Enemy_Script square = other.GetComponent<Enemy_Script>();
            square.startHypnosis(duration);
        }
        else if (other.tag == "Triangle Enemy")
        {
            Enemy_Script triangle = other.GetComponent<Enemy_Script>();
            triangle.startHypnosis(duration);
        }
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
    }
}
