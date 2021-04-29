using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSwordScript : MonoBehaviour
{
    public GameObject Gobj;

    int Damage = 0;

    bool DelayOn = false;

    float DelayTime = 0.0f;

    float Delay = 0.0f;

    void Start()
    {
        Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        if(DelayOn == true)
        {
            Delay += Time.deltaTime;
            if(Delay >= DelayTime)
            {
                DelayOn = false;
                Delay = 0.0f;
                DelayTime = 0.0f;
            }
        }

        if(SkeletonScript.SkillOn == 1)
        {
            if (DelayOn == false)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                Damage = SkeletonScript.SkeletonDamage;
                DelayTime = 0.5f;
            }
        }
        else if(SkeletonScript.SkillOn == 2)
        {
            if (DelayOn == false)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
                Damage = (int)(SkeletonScript.SkeletonDamage * 1.5);
                DelayTime = 1f;
            }
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
        DelayOn = true;
    }
}
