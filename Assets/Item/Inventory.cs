using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public static bool inventory
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots; //슬롯 들
    private Slot slot;

    // Update is called once per frame
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); //슬롯20개 가져와서 지정
    }

    public void hp_set()
    {
        
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null) // 이미 획득한 아이템일 경우 , 숫자만 늘리기
                {
                    if (slots[i].item.itemName == "HP_Potion")
                    {
                        slots[i].SetSlotCount(-1);
                         Player_Scr.HP += 30;
                        Debug.Log("hp획득");
                        return;
                    }
                }

            }        
    }
    public void mp_set()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) // 이미 획득한 아이템일 경우 , 숫자만 늘리기
            {
                if (slots[i].item.itemName == "MP_Potion")
                {
                    slots[i].SetSlotCount(-1);
                    Player_Scr.MP += 30;
                    Debug.Log("hp획득");
                    return;
                }
            }

        }
    }

    public void qqqq()
    {
        if(slot.item.itemName == "ATK_Book")
        {
            Debug.Log("스크롤획득");
        }
    }




    public void AcquireItem(Item _item, int _count) //아이템 획듯시 실행
    {
        if(Item.ItemType.Equipment != _item.itemType) //장비이 아닐 경우
        {
            for (int i = 0; i < slots.Length; i++) 
            {
                if(slots[i].item != null) // 이미 획득한 아이템일 경우 , 숫자만 늘리기
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
                
            }
        }

        

        for (int i = 0; i < slots.Length; i++) //슬롯들 전부 검사
        {
            if (slots[i].item == null) //먹은 아이템이 슬롯에 없으면 슬롯에 추가
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
