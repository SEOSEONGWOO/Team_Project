using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider sliderHP;

    void Update()
    {
        sliderHP.value = UsurperSkill.DrgHP / UsurperSkill.MaxDrgHP;
    }
}
