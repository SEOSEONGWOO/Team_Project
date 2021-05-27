using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPBar : MonoBehaviour
{
    public Slider sliderMP;

    void Update()
    {
        sliderMP.value = Player_Scr.MP / Player_Scr.maxMP;
    }
}
