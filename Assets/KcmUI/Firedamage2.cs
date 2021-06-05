using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firedamage2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("타죽음");
            Player_Scr.HP = 0;
        }
    }
}
