using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_count; //아이템 갯수  수정
    [SerializeField]
    private GameObject go_CountImage; // 아이템 갯수 이미지 띄우기
    
    public void AddItem(Item _item, int _count = 1)//아이템 획득 시 실행
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment)
        {
            text_count.text = itemCount.ToString();
            go_CountImage.SetActive(true);
            
        }
        else
        {
            
            text_count.text = "0";
            go_CountImage.SetActive(false);
        }

    }
    public void SetSlotCount(int _count)//아이템 갯수 조정
    {
        itemCount += _count;
        text_count.text = itemCount.ToString();

        if (itemCount <= 0) ClearSlot(); //아이템이 0개가 되면
    }

    void ClearSlot() //슬롯 초기화
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        
        text_count.text = "0";
        go_CountImage.SetActive(false);

    }


}
