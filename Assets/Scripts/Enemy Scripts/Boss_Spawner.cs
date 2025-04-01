using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spawner : MonoBehaviour
{
    [Header("Values")]
    public float timer;
    public bool enemyChosen;
    
    [Header("Objects")]
    public GameObject Boss;
    
    [Header("Particle")]
    public ParticleSystem particle;
    
    [Header("Scripts")]
    public Audio_Manager AM;
    public Wave_Manager WM;
    public Screen_Shake screenShake;

    [Header("Colors")]
    public SpriteRenderer SR;
    public Color normalColor;
    
    private void Start()
    {
        timer = 2f;
        normalColor = SR.color;
        
        AM = GameObject.FindWithTag("Audio Manager").GetComponent<Audio_Manager>();
        WM = GameObject.FindWithTag("Game Manager").GetComponent<Wave_Manager>();
        screenShake = GameObject.FindWithTag("MainCamera").GetComponent<Screen_Shake>();

        StartCoroutine(warningFlash());
    }

    void Update()
    {
        //Spawn timer counts down and then spawns at 0//
        timer -= Time.deltaTime;
        
        if (timer < 0)
        {
            WM.addList(Instantiate(Boss, new Vector2(-.16f,1.48f), Quaternion.identity));
            
            //Upon spawn the spawner is destroyed and a particle and sound is placed
            screenShake.setShakeTime(.2f);
            Instantiate(particle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            AM.playSound(2,.5f);
            
            Destroy(gameObject);
        }
    }

    public IEnumerator warningFlash()
    {
        while (timer > 0)
        {
            SR.color = Color.red;
            yield return new WaitForSeconds(.3f);
            SR.color = normalColor;
            yield return new WaitForSeconds(.3f);
        }
    }
}
