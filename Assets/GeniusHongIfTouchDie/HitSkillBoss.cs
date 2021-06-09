using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSkillBoss : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Skill1")
        {
            UsurperSkill.DrgHP -= 100;
            Debug.Log(UsurperSkill.DrgHP);
        }

        if (other.tag == "skill1_2")
        {
            UsurperSkill.DrgHP -= 30;
            Debug.Log(UsurperSkill.DrgHP);

        }
        if (other.tag == "Skill1_4")
        {
            UsurperSkill.DrgHP -= 100;
        }

        if (other.tag == "Skill2_1")
        {
            UsurperSkill.DrgHP -= 100;
        }

        if (other.tag == "Skill2_3")
        {
            UsurperSkill.DrgHP -= 100;

        }
        if (other.tag == "Skill2_4")
        {
            UsurperSkill.DrgHP -= 100;

        }
        if (other.tag == "Skill3_1")
        {
            UsurperSkill.DrgHP -= 100;

        }
        if (other.tag == "Skill3_2")
        {
            UsurperSkill.DrgHP -= 100;
        }
    }
}
