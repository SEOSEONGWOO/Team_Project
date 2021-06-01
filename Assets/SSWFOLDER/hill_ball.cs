using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hill_ball : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("힐 !");
            Player_Scr.HP += 50;
            Destroy(gameObject);
        }
    }
    
}
