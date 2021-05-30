using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject inventoryPanel;
    bool activeInventory = false;

    [SerializeField]
    private GameObject go_SlotParent; //슬롯 전체저장

    private Slot[] slots; //슬롯들

    void Start()
    {
        slots = go_SlotParent.GetComponentsInChildren<Slot>();

        inventoryPanel.SetActive(activeInventory);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }


    }
    public void AcquireItem(Item _item , int _count = 1)
    {
        Debug.Log("오냐?");
       
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
            
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemName == null)
            {
                slots[i].AddItem(_item , _count);
                return;
            }
        }
    }

    

    




}
