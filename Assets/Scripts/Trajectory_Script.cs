using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory_Script : MonoBehaviour
{
    // Also from: https://youtu.be/Tsha7rp58LI?si=xKviVD4YuTc3zCc4 //
    public LineRenderer LR;

    private void Awake()
    {
        LR = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        LR.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;
        
        LR.SetPositions(points);
    }

    public void Render(bool state)
    {
        LR.enabled = state;
    }

    public void changeColor(Gradient gradient)
    {
        LR.colorGradient = gradient;
    }
}
