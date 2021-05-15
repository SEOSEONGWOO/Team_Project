using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelMove : MonoBehaviour
{
    public GameObject SkillPanel1;
    public GameObject SkillPanel2;
    public GameObject SkillPanel3;


    public int SkillPanelChange = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
         {
            SkillPanelChange -= 1;
         }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SkillPanelChange += 1;
        }
        if(SkillPanelChange == 1)
        {
            SkillPanel1.SetActive(true);
            SkillPanel2.SetActive(false);
            SkillPanel3.SetActive(false);
        }
        if (SkillPanelChange == 2)
        {
            SkillPanel1.SetActive(false);
            SkillPanel2.SetActive(true);
            SkillPanel3.SetActive(false);
        }
        if (SkillPanelChange == 3)
        {
            SkillPanel1.SetActive(false);
            SkillPanel2.SetActive(false);
            SkillPanel3.SetActive(true);
        }
        if(SkillPanelChange > 3)
        {
            SkillPanelChange = 1;
        }
        if(SkillPanelChange < 1)
        {
            SkillPanelChange = 3;
        }
    }
}
