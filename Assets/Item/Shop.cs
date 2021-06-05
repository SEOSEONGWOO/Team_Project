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
    public GameObject holly_Ball;

    public void ATK_scoll_get()
    {
        theInventory.AcquireItem(atk_scoll.GetComponent<ItemPickUp>().item, 1);
        Debug.Log("아이템 구매");
    }
    public void DEF_scoll_get()
    {
        theInventory.AcquireItem(def_scoll.GetComponent<ItemPickUp>().item, 1);
    }
    public void HP_Potion_get()
    {
        theInventory.AcquireItem(hp_potion.GetComponent<ItemPickUp>().item, 1);
    }
    public void MP_Potion_get()
    {
        theInventory.AcquireItem(mp_potion.GetComponent<ItemPickUp>().item, 1);
    }
    public void Holly_ball()
    {
        theInventory.AcquireItem(holly_Ball.GetComponent<ItemPickUp>().item, 1);
        SkillPanelMove.holly = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
