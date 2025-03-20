using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar_Script : MonoBehaviour
{
    ////// Code used https://youtu.be/_lREXfAMUcE?si=3Tx17rTv2eaKRp5l \\\\\\\
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void UpdateHealthBar(float current, float max)
    {
        slider.value = current / max;
    }

    public void Update()
    {
        //Ensures the healthbar is facing the camera and is always positioned on top of the enemy
        transform.parent.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
