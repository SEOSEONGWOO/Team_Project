using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject Gobj;

    int Damage = 99999999;

    void Start()
    {
        Gobj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
            Debug.Log("okokokokokokookkokokokoko배고파");
        }
        else if (other.tag == "Enemy")
        {
            gameObject.active = false;
        }
    }
}
