using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    public GameObject Gobj;

    int Damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Gobj = GameObject.Find("HPCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        if(Skeleton.AttackMotion == 1)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            Damage = 30;
        }
        else if(Skeleton.AttackMotion == 2)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            Damage = 50;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
