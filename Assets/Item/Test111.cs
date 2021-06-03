using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test111 : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item, 1);
           // Destroy(other);
        }
    }
}
