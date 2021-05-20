using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKey : MonoBehaviour
{
    public GameObject Question;
    public static GameObject panel;

    public bool panelonoff = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Question.SetActive(true);
          
            if(Question == true)
            {
                if(Input.GetKeyDown(KeyCode.G))
                {

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    panelonoff = !panelonoff;
                    panel.SetActive(panelonoff);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Question.SetActive(false);
            panel.SetActive(false);
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
        
    }
}
