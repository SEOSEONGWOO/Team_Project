using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEat : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            theInventory.AcquireItem(gameObject.GetComponent<ItemPickUp>().item, 1);
            Destroy(gameObject);
        }
    }*/
}
