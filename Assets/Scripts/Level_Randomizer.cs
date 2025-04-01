using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Randomizer : MonoBehaviour
{
    [Header("Levels")] 
    public List<GameObject> levelList;
    public List<GameObject> bossLevelList;
    public int random;
    
    [Header("Background Colors")]
    public Camera mainCam;
    public Color bossColor;
    void Start()
    {
        levelRandomize();
    }

    public void levelRandomize()
    {
        levelsOff();
        randomizeColor();
        
        random = Random.Range(0, levelList.Count);

        levelList[random].SetActive(true);
    }

    public void bossLevelRandomize()
    {
        levelsOff();
        mainCam.backgroundColor = bossColor;
        
        random = Random.Range(0, bossLevelList.Count);

        bossLevelList[random].SetActive(true);
    }

    public void levelsOff()
    {
        //Normal levels
        foreach (GameObject level in levelList)
        {
            level.SetActive(false);
        }
        //For boss levels
        foreach (GameObject bossLevel in bossLevelList)
        {
            bossLevel.SetActive(false);
        }
    }

    public void randomizeColor()
    {
        // Got from https://discussions.unity.com/t/how-to-generate-a-random-color/198158/8 //
        byte r = (byte)Random.Range(64, 125);
        byte b = (byte)Random.Range(64, 125);
        byte g = (byte)Random.Range(64, 125);
        Color32 color = new Color32(r,b,g,255);

        mainCam.backgroundColor = color;
    }
}
