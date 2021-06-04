using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    [SerializeField]
    private Inventory theInventory;

    public GameObject atk_scoll;
    public GameObject def_scoll;
    public GameObject hp_potion;
    public GameObject mp_potion;
    public GameObject Holly_Ball;

    public void ATK_scoll_get()
    {
        theInventory.AcquireItem(atk_scoll.GetComponent<ItemPickUp>().item, 1);
        Debug.Log("아이템 구매");
    }
    void ATK1_scoll_get()
    {
        theInventory.AcquireItem(atk_scoll.GetComponent<ItemPickUp>().item, 1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
