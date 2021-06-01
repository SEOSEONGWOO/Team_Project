using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrgAttSC : MonoBehaviour
{
    public GameObject Gobj;

    public GameObject Flame1;
    public GameObject Flame2;

    int Damage = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Gobj = GameObject.FindWithTag("Player");

        if (UsurperSkill.AttackOn == 1)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Damage = 30;
        }
        else if(UsurperSkill.AttackOn == 2)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            Damage = 50;
        }
        else if(UsurperSkill.AttackOn == 0)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Damage = 0;
        }

        if(UsurperSkill.FireOn == 1)
        {
            Flame1.SetActive(true);
        }
        else if(UsurperSkill.FireOn == 2)
        {
            Flame2.SetActive(true);
        }
        else if(UsurperSkill.FireOn == 0)
        {
            Flame1.SetActive(false);
            Flame2.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
