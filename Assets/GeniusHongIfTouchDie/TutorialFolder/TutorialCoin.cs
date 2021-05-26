using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player_Scr.PlayerMoney += 500;
            Destroy(gameObject);
        }
    }
}
