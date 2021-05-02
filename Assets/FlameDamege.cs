using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDamege : MonoBehaviour
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
        if(other.tag == "Player")
        {
            Debug.Log("구워지는중");
            Player_Scr.HP = 0;
        }
    }
}
