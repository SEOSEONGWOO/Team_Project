using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KissOrcWeapon : MonoBehaviour
{
    public GameObject Gobj;

    int Damage = 0;

    float Delay = 2f;

    float Cool = 0f;

    int Mode = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Gobj = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log(Mode);

        if (Mode == 1)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            Cool += Time.deltaTime;
            if (Cool >= Delay)
            {
                Cool = 0f;
                Mode = 0;
            }
        }
        else if (Mode == 2)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else if (Mode == 0)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if (KissOrc.AttackMotion == 1)
        {
            Mode = 1;
            Damage = 30;
        }
        else if (KissOrc.AttackMotion == 2)
        {
            Mode = 2;
            Damage = 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Mode == 1)
            {
                Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
                Mode = 0;
                Cool = 0f;
                Debug.Log("Mode 1 : " + gameObject.name);
            }
            else if (Mode == 2)
            {
                Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Damage);
                Cool += Time.deltaTime;
                if (Cool >= Delay)
                {
                    Cool = 0f;
                    Mode = 0;
                }
                Debug.Log("Mode 2: " + gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
