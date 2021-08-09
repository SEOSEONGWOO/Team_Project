using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKey : MonoBehaviour
{
    public GameObject Question;
    public GameObject panel;
    public AudioClip clip;

    public GameObject inventoryPnl;

    public bool panelonoff = false;

    public static bool isPnlOn;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isPnlOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Question.SetActive(false);
            panel.SetActive(false);
            isPnlOn = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Question.SetActive(false);
        panel.SetActive(panelonoff);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPnlOn)
        {
            Question.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("g버튼");

                if (Question == true)
                {
                    SoundManager.instance.SFXPlay("Shop", clip);
                    Debug.Log("버튼");
                    if (Player_Scr.isShop)
                    {
                        //상점 활성화일때 Locomotion(), Fight(), Jump(), roll() 비활성화

                        Player_Scr.isShop = false;
                    }
                    else if (!Player_Scr.isShop)
                    {
                        //상점 비활성화일때 Locomotion(), Fight(), Jump(), roll() 활성화
                        Player_Scr.isShop = true;
                    }


                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    panelonoff = !panelonoff;
                    panel.SetActive(panelonoff);
                    inventoryPnl.SetActive(panelonoff);
                }
            }
        }
        if (panel.activeSelf) { CameraCs2.cc = false; }

        else if(panel.activeSelf == false && InventoryUI.isInven == false) { 
            CameraCs2.cc = true;
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
        }
                
    }
}
