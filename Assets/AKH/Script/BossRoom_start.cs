using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom_start : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("BossRoom_start");
            Playerspawn.isBossRoom = true;
        }
    }
}
