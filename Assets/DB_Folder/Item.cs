using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;  //아이템 이름
    public ItemType itemType;
    public Sprite itemImage; // 아이템의 이미지
    public GameObject itemPrefeb; // 아이템의 프리펩
   // public int Price; //아이템의 가격

    //public string weaponType; //무기 유형
    // Start is called before the first frame update

    public enum ItemType
    {
        Equipment,  //장비 아이템
        Used //사용품
    }
    
}
