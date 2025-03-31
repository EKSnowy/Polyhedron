using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOption_Script : MonoBehaviour
{
    public bool isMax;

    public bool getMax()
    {
        return isMax;
    }

    public void setMax(bool b)
    {
        isMax = b;
    }
}
