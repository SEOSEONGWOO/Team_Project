﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartText4 : MonoBehaviour
{
    public GameObject textPanel;
    public bool textStart = false;
 
    public float TextTime = 0.0f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
 
            textPanel.SetActive(true);
            textStart = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(textStart == true)
        {
            TextTime += Time.deltaTime;
            if (TextTime > 4)
            {
                textPanel.SetActive(false);
                textStart = false;
                TextTime = 0;
                Destroy(gameObject);
            }
        }
        
    }
}
