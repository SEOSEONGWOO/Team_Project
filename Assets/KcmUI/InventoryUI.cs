using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject inventoryPanel;
    bool activeInventory = false;
    public AudioClip clip;
    public static bool isInven = false;

    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (Player_Scr.isShop)
            {
                //인벤 활성화일때 Locomotion(), Fight(), Jump(), roll() 비활성화
                isInven = true;
                Player_Scr.isShop = false;
            }
            else if (!Player_Scr.isShop)
            {
                //인벤 비활성화일때 Locomotion(), Fight(), Jump(), roll() 활성화
                isInven = false;
                Player_Scr.isShop = true;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SoundManager.instance.SFXPlay("Inventory", clip);
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        if (inventoryPanel.activeSelf) { CameraCs2.cc = false; }
        
        else if (!inventoryPanel.activeSelf) { CameraCs2.cc = true; }

    }
}
