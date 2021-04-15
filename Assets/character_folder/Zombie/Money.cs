using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Gunner.GunnerMoney = Gunner.GunnerMoney + Random.Range(1, 10);
            Destroy(gameObject);
        }
    }
}
