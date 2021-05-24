using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Gobj;

    void Start()
    {
        Gobj = GameObject.Find("HPCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player_Scr.FireM = true;
            Player_Scr.FireTime = 0.0f;
        }
    }
}
