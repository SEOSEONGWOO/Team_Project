using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelMove : MonoBehaviour
{
    public GameObject SkillPanel1;
    public GameObject SkillPanel2;
    public GameObject SkillPanel3;

    public GameObject SkillPanelnone;
    public GameObject fireLock;
    public GameObject lightningLock;
    public GameObject hollyLock;

    public int SkillPanelChange;

    public AudioClip clip;

    public static bool fire = false;
    public static bool lightning = false;
    public static bool holly = false;

    public static int skill_mode = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (fire == true)
        {
            SkillPanelChange = 0;
        }
        if (lightning == true)
        {
            SkillPanelChange = 1;
        }
        if (holly == true)
        {
            SkillPanelChange = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            SkillPanelChange -= 1;
            SoundManager.instance.SFXPlay("change", clip);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SkillPanelChange += 1;
            SoundManager.instance.SFXPlay("change", clip);
        }

            if (fire == true)
            {
                SkillPanelnone.SetActive(false);
                SkillPanel1.SetActive(true);
                SkillPanel2.SetActive(false);
                SkillPanel3.SetActive(false);
                skill_mode = 1;
            }
            if (SkillPanelChange == 1)
            {
                if (lightning == true)
                {
                    SkillPanel1.SetActive(false);
                    SkillPanel2.SetActive(true);
                    SkillPanel3.SetActive(false);
                    skill_mode = 2;
            }

            }
            if (SkillPanelChange == 2)
            {
                if (holly == true)
                {
                    SkillPanel1.SetActive(false);
                    SkillPanel2.SetActive(false);
                    SkillPanel3.SetActive(true);
                    skill_mode = 3;
            }

            }
        if (SkillPanelChange >= 3)
        {
            SkillPanelChange = 0;
        }
        if (SkillPanelChange <= -1)
        {
            if(holly == true)
            {
                SkillPanelChange = 2;
            }          
        }
        if (fire == true)
            {
                fireLock.SetActive(false);

            }
            if (lightning == true)
            {
                lightningLock.SetActive(false);
            }
            if (holly == true)
            {
                hollyLock.SetActive(false);
            }

            if (fire == false)
            {
                fireLock.SetActive(true);
            }
            if (lightning == false)
            {
                lightningLock.SetActive(true);
            if (SkillPanelChange == 1)
            {
                SkillPanelChange = 0;
            }
            if (SkillPanelChange == -1)
            {
                SkillPanelChange = 0;
            }
        }
            if (holly == false)
            {
                hollyLock.SetActive(true);
              if(SkillPanelChange == 2)
              {
                SkillPanelChange = 0;
              }
              if(SkillPanelChange == -1)
             {
                SkillPanelChange = 1;
             }
            }


        }

    }
   
