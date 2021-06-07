using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    private void Start()
    {
        theInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Destroy(other.gameObject);
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item, 1);  
        }

        if (other.tag == "fire")
        {
            SkillPanelMove.fire = true;
            ShootSimple_Scr.WeaponNumber = 2;
            Destroy(other.gameObject);
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item, 1);
        }

        if (other.tag == "Ice")
        {
            SkillPanelMove.lightning = true; 
            Destroy(other.gameObject);
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item, 1);
        }

        if (other.tag == "Money")
        {
            Destroy(other.gameObject);
            Player_Scr.money += 100;
        }

    }
}
