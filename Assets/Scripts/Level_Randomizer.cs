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
    public List<Color> colorList;
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
        random = Random.Range(0, colorList.Count);

        mainCam.backgroundColor = colorList[random];
    }
}
