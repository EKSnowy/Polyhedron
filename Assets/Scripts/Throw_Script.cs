using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Script : MonoBehaviour
{
   // Drag and throw script: https://youtu.be/Tsha7rp58LI?si=_dUqNFkhLBhb24Tp //
   public float throwPower;
   public Rigidbody2D RB;

   public Vector2 minPower;
   public Vector2 maxPower;

   public Camera cam;
   public Vector2 force;
   public Vector3 startPoint;
   public Vector3 endPoint;
   public bool throwToggle;

   Trajectory_Script TS;
   public Vector2 minVector;
   public Vector2 maxVector;
   public Vector2 currentPos;
   
   private void Start()
   {
       cam = Camera.main;
       TS = GetComponent<Trajectory_Script>();
   }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Slows down time when choosing direction//
            Time.timeScale =.2f;
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
            TS.Render(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            TS.RenderLine(startPoint,currentPoint);
            currentPos = transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Resumes time and uses direction to apply force//
            Time.timeScale = 1f;
            RB.velocity = Vector2.zero;
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            
            TS.Render(false);
            
            calculateForce();
        }
        
        //////Checks if the force has hit the cap speed///////
        
        //If velocity is lower than min X
        if (RB.velocity.x <= minVector.x)
        {
            RB.velocity = new Vector2(minVector.x,RB.velocity.y);
        }
        //If velocity is lower than min Y
        else if (RB.velocity.y <= minVector.y)
        {
            RB.velocity = new Vector2(RB.velocity.x,minVector.y);
        }
        //If velocity is higher than max X
        else if (RB.velocity.x >= maxVector.x)
        {
            RB.velocity = new Vector2(maxVector.x,RB.velocity.y);
        }
        //If velocity is higher than max Y
        else if (RB.velocity.y >= maxVector.y)
        {
            RB.velocity = new Vector2(RB.velocity.x,maxVector.y);
        }
    }
   
   public void calculateForce()
   {
       Vector2 distance = (startPoint - endPoint);

       force = distance * throwPower;
       
       /*force = new Vector2(Mathf.Clamp(distance.x, minPower.x, maxPower.x), 
           Mathf.Clamp(distance.y, minPower.y, maxPower.y));*/
        
       RB.AddForce(force * throwPower, ForceMode2D.Force);
       transform.position = currentPos;
   }
   
}
