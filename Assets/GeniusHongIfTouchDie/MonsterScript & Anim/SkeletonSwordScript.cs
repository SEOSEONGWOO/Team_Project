using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSwordScript : MonoBehaviour
{
    public GameObject Gobj;

    int Damage = 0;

    void Start()
    {
        Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        if(SkeletonScript.SkillOn == 1)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Damage = SkeletonScript.SkeletonDamage;
        }
        else if(SkeletonScript.SkillOn == 2)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Damage = (int)(SkeletonScript.SkeletonDamage * 1.5);
        }
        else if(SkeletonScript.SkillOn == 0)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Damage = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}
