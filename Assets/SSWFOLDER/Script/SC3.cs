using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC3 : MonoBehaviour
{
    public GameObject ui;
    public GameObject text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<TextMeshProUGUI>().text = Rank_DB.score_bd[2];
    }
}
