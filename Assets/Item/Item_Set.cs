using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Set : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;
    // Start is called before the first frame update
    private void Start()
    {
        theInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Update()
    {
        Set_Item();
    }
    public void Set_Item()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            theInventory.hp_set();
            Debug.Log("q눌림");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            theInventory.mp_set();
        }
    }
}
