using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{

    public GameObject startUI;

    public static bool StartUI1 = false;

    // Start is called before the first frame update
    void Start()
    {
        if (StartUI1 == true)
        {
            startUI.SetActive(true);

            StartUI1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
