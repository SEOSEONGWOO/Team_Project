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
}
