using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Throw_Script : MonoBehaviour
{
   // Drag and throw script: https://youtu.be/Tsha7rp58LI?si=_dUqNFkhLBhb24Tp //
   
   public float throwPower;
   public Rigidbody2D RB;
   public Camera cam;
   
   [Header("Vectors")]
   public Vector2 force;
   public Vector3 startPoint;
   public Vector3 endPoint;

   [Header("Line Color Gradients")]
   public Gradient throwGradient;
   public Gradient dashGradient;

   [Header("Dash")]
   public float dashCooldown;
   public TextMeshPro dashText;
   public Image dashFill;
   public bool soundCooldown;

   [Header("Scripts")]
   Trajectory_Script TS;
   public Player_Script player;
   public Audio_Manager AM;
   public AudioSource Music;
   
   private void Start()
   {
       cam = Camera.main;
       TS = GetComponent<Trajectory_Script>();
       
       AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
       dashCooldown = -1;
       soundCooldown = true;
   }

   void Update()
    {
        if (player.getHealth() > 0)
        {
            ///////////THROW//////////////
        //Left mouse button moves player via click and drag
        if (Input.GetMouseButtonDown(0))
        {
            //Slows down time when choosing direction//
            Time.timeScale =.2f;
            Music.volume = .2f;
            Music.pitch = .98f;
            
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
            
            TS.changeColor(throwGradient);
            TS.Render(true);
        }

        //Renders line when mouse is pressed
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 0;
            TS.RenderLine(startPoint,currentPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Resumes time and uses direction to apply force//
            Time.timeScale = 1f;
            Music.volume = .35f;
            Music.pitch = 1f;
            
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
                //Slows down time when choosing direction//
                Time.timeScale =.2f;
                Music.volume = .2f;
                Music.pitch = .98f;
            
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
                //Resumes time//
                Time.timeScale = 1f;
                Music.volume = .35f;
                Music.pitch = 1f;
                
                AM.playSound(3,1);
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 0;

                player.setInvTimer(.5f);
                transform.position = endPoint;
                calculateDash();
                TS.Render(false);
            }
        }
        
        }
        
        ///////////Dash Cooldown///////////////
        if (dashCooldown >= 0)
        {
            dashCooldown -= Time.deltaTime;
            dashFill.fillAmount -= (float) 1 / 3 * Time.deltaTime;
            
            float roundedDashTime = (float)((Mathf.Round(dashCooldown * 10)) / 10.0);;
            dashText.text = "" + roundedDashTime;
            soundCooldown = false;
        }
        else
        {
            dashFill.fillAmount = 0;
            dashText.text = "";

            if (!soundCooldown)
            {
                AM.playSound(3,1.5f);
                soundCooldown = true;
            }
        }
    }
   //For throwing
   public void calculateForce()
   {
       if (startPoint != endPoint)
       {
           RB.velocity = Vector2.zero;

           Vector2 distance = (startPoint - endPoint) / 25;

           force = distance * throwPower;

           RB.AddForce(force, ForceMode2D.Force);
       }
   }
       
       
   //For dashing
   public void calculateDash()
   {
       if (startPoint != endPoint)
       {
           RB.velocity = Vector2.zero;
           
           Vector2 distance = (endPoint - startPoint) / 25;
           force = distance * throwPower;
           
           RB.AddForce(force, ForceMode2D.Force);
           
           dashCooldown = 3;
           dashFill.fillAmount = 1;
       }
   }
}
