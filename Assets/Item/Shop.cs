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
        if (Player_Scr.money >= 300)
        {
            theInventory.AcquireItem(atk_scoll.GetComponent<ItemPickUp>().item, 1);
            Player_Scr.money -= 300;
            Debug.Log(Player_Scr.money);
        }
    }
    public void DEF_scoll_get()
    {
        if (Player_Scr.money >= 300)
        {
            theInventory.AcquireItem(def_scoll.GetComponent<ItemPickUp>().item, 1);
            Player_Scr.money -= 300; Debug.Log(Player_Scr.money);
        }
        
    }
    public void HP_Potion_get()
    {
        if (Player_Scr.money >= 300)
        {
            theInventory.AcquireItem(hp_potion.GetComponent<ItemPickUp>().item, 1);
            Player_Scr.money -= 300; Debug.Log(Player_Scr.money);
        }
        
    }
    public void MP_Potion_get()
    {
        if (Player_Scr.money >= 300)
        {
            theInventory.AcquireItem(mp_potion.GetComponent<ItemPickUp>().item, 1);
            Player_Scr.money -= 300; Debug.Log(Player_Scr.money);
        }
        
    }
    public void Holly_ball()
    {
        if (Player_Scr.money >= 500)
        {
            theInventory.AcquireItem(holly_Ball.GetComponent<ItemPickUp>().item, 1);
            Player_Scr.money -= 500; Debug.Log(Player_Scr.money);
        }
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
