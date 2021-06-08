using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clear_UI : MonoBehaviour
{
    public GameObject text;
    void Start()
    {
        text.GetComponent<Text>().text = Player_Scr.clear_time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<Text>().text = Player_Scr.clear_time.ToString();
    }
}
