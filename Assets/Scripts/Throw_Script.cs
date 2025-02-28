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
   
   private void Start()
   {
       cam = Camera.main;
       TS = GetComponent<Trajectory_Script>();
   }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        }

        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1f;
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), 
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            TS.Render(false);
            throwToggle = true;
        }
    }

   private void FixedUpdate()
   {
       if (throwToggle)
       {
           RB.AddForce(force * throwPower, ForceMode2D.Force);
           throwToggle = false;
       }
   }
}
