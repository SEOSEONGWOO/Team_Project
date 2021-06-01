using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitUsurper : MonoBehaviour
{
    public void SetDamageAI()
    {
        UsurperSkill.DrgHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(UsurperSkill.DrgHP);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Skill1")
        {
            UsurperSkill.DrgHP -= 500;
        }

        if (other.tag == "skill1_2")
        {
            UsurperSkill.DrgHP -= 500;

        }
        if (other.tag == "Skill1_4")
        {
            UsurperSkill.DrgHP -= 500;
        }

        if (other.tag == "Skill2_1")
        {
            UsurperSkill.DrgHP -= 500;
        }

        if (other.tag == "Skill2_3")
        {
            UsurperSkill.DrgHP -= 500;

        }
        if (other.tag == "Skill2_4")
        {
            UsurperSkill.DrgHP -= 500;

        }
        if (other.tag == "Skill3_1")
        {
            UsurperSkill.DrgHP -= 500;

        }
        if (other.tag == "Skill3_2")
        {
            UsurperSkill.DrgHP -= 500;
        }
    }
}
