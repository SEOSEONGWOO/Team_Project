using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public static bool inventory
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots; //슬롯 들

    // Update is called once per frame
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); //슬롯20개 가져와서 지정
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
