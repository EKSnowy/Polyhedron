using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{

    public void startButton()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void tutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void menuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
