using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rotation : MonoBehaviour
{
    public float rotationSpeed;
    public Transform target;
    void Update()
    {
        this.transform.RotateAround(target.position,Vector3.forward,rotationSpeed*Time.deltaTime);
    }

    public void setRotationSpeed(float num)
    {
        rotationSpeed = num;
    }

    public void switchRotation()
    {
        rotationSpeed *= -1;
    }
}
