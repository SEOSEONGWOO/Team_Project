using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item" , menuName="New Item/item")]
public class Item : ScriptableObject  //이거 붙이면 오브젝트에 스크립트 안붙여도 사용가능
{
    public string itemName;
    public Sprite itemImage;
    public ItemType itemType; 

    public enum ItemType
    {
        Equipment,
        Used
    }
    private void Start()
    {
        itemType = ItemType.Equipment;
    }
}
