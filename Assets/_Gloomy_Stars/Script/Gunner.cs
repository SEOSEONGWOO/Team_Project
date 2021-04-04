using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Gunner : MonoBehaviour
{
    public Animator avatar;

    float GunnerHP = 100f;


    public Transform clcl;

    public Slider GunnerHPBar;

    public static int GunnerMoney;

    public GameObject GunnerMoneyText;

    public static Vector3 CLC;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CLC = clcl.transform.position;

        string GunnerMoneyT = "보유 금액 : " + GunnerMoney;
        GunnerMoneyText.GetComponent<Text>().text = GunnerMoneyT;

        GunnerHPBar.value = GunnerHP / 100f;

        if (Input.GetButtonDown("Fire1"))
        {
            GunnerHP -= 10;
        }

        if (GunnerHP == 0)
        {

        }


    }
}
