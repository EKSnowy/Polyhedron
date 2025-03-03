using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Throw_Script : MonoBehaviour
{
   // Drag and throw script: https://youtu.be/Tsha7rp58LI?si=_dUqNFkhLBhb24Tp //
   public float throwPower;
   public Rigidbody2D RB;
   
   public Camera cam;
   public Vector2 force;
   public Vector3 startPoint;
   public Vector3 endPoint;

   Trajectory_Script TS;
   public Vector2 currentPos;

   public Gradient throwGradient;
   public Gradient dashGradient;

   public float dashCooldown;
   public TextMeshPro dashText;

   public Player_Script player;
   
   private void Start()
   {
       cam = Camera.main;
       TS = GetComponent<Trajectory_Script>();
   }

   void Update()
    {
        ///////////THROW//////////////
        //Left mouse button moves player via click and drag
        if (Input.GetMouseButtonDown(0))
        {
            //Slows down time when choosing direction//
            Time.timeScale =.2f;
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
            
            TS.changeColor(throwGradient);
            TS.Render(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 0;
            TS.RenderLine(startPoint,currentPoint);
            currentPos = transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Resumes time and uses direction to apply force//
            Time.timeScale = 1f;
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;
            
            TS.Render(false);
            
            calculateForce();
        }
        
        //////////DASH//////////////
        /// //Right mouse button allows player to dash
        /// Can only dash after the cooldown is up
        if (dashCooldown <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Time.timeScale =.2f;
            
                TS.changeColor(dashGradient);
                TS.Render(true);
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 0;
                TS.RenderLine(gameObject.transform.position,currentPoint);
            }
            //Player teleports to end point as a dash, gains invincibility, and activates cooldown
            if (Input.GetMouseButtonUp(1))
            {
                Time.timeScale = 1f;
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 0;

                transform.position = endPoint;
                calculateDash();
                TS.Render(false);
            }
        }
        
        //Placeholder to show the cooldown
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
            dashText.text = "Cooldown: " + dashCooldown;
        }
    }
   //For throwing
   public void calculateForce()
   {
       if (startPoint != endPoint)
       {
           RB.velocity = Vector2.zero;
           
           Vector2 distance = (startPoint - endPoint);
           force = distance.normalized * throwPower;
           
           RB.AddForce(force * throwPower, ForceMode2D.Force);
           transform.position = currentPos;
       }
       
       /*force = new Vector2(Mathf.Clamp(distance.x, minPower.x, maxPower.x), 
           Mathf.Clamp(distance.y, minPower.y, maxPower.y));*/
   }
   //For dashing
   public void calculateDash()
   {
       if (startPoint != endPoint)
       {
           RB.velocity = Vector2.zero;
           
           Vector2 distance = (endPoint - startPoint);
           force = distance.normalized * throwPower;
           
           RB.AddForce(force * throwPower, ForceMode2D.Force);
           
           dashCooldown = 3;
           player.setInvTimer(.5f);
       }
   }
}
