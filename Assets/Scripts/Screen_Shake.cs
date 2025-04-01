using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Shake : MonoBehaviour
{
    public float Intensity = .1f;
    
    //A countdown, while this is > 0 the screen shakes
    public float ShakeTimer;
    
    //Where it was before it shook
    public Vector3 StartPos;
    private void Start()
    {
        //Record our start position
        StartPos = transform.position;
    }

    void Update()
    {
            if (ShakeTimer > 0)
            {
                //Make it slowly count down
                ShakeTimer -= Time.deltaTime;
                //Calculate how much shake we want this frame
                Vector3 shake = new Vector3(Random.Range(-Intensity, Intensity),
                    Random.Range(-Intensity, Intensity));
                //Set its position to be its start position plus the shake offset
                transform.position = StartPos + shake;
            }
            else //When we're done, go back to our start position
            {
                transform.position = StartPos;
            }
    }

    public void setShakeTime(float num)
    {
        ShakeTimer = num;
    }
}
