using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
