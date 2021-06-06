using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Player_Scr.PlayerMoney = Player_Scr.PlayerMoney + Random.Range(50, 200);
            Destroy(gameObject);
        }
    }
}
