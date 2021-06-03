using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsT : MonoBehaviour
{
    public GameObject Gobj;

    public GameObject Tooth;

    private void Update()
    {
        Gobj = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Gobj.GetComponent<Player_Scr>().GunnerHitFunc(DrgAttSC.Damage);
            Tooth.SetActive(false);
            DrgAttSC.Mode1 = 0;
        }
    }
}
