using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; //획득 아이템
    public int itemCount; //아이템 갯수
    public Image itemImage; //획득 아이템 이미지

    [SerializeField]
    private Text text_count; //아이템 갯수 출력 텍스트 
    [SerializeField]
    private GameObject go_CountImage; //획득 아이템 이미지;


    public void AddItem(Item _item, int _count = 1)
    { 
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        go_CountImage.SetActive(true);
        text_count.text = itemCount.ToString();
    }
    // Start is called before the first frame update
    
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_count.text = itemCount.ToString();

        if (itemCount <= 0) ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;

        go_CountImage.SetActive(false);
        text_count.text = "0";
    }
}
