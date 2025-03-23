using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Script : MonoBehaviour
{
    public float speed;
    public string rotationType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationType == "Shields")
        {
           float rotAmount = speed * Time.deltaTime;
           float curRot = transform.localRotation.eulerAngles.z;
           transform.localRotation = Quaternion.Euler(new Vector3(0,0,curRot+rotAmount)); 
        }
         else if (rotationType == "Aim")
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var offset = 270f;
            Vector2 direction = mousePos - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
    }
}
