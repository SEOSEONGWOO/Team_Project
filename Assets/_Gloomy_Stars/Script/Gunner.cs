using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Gunner : MonoBehaviour
{
    public Animator avatar;

    float GunnerHP = 100f;
    int GunnerA = 10; //공격력
    int GunnerD = 5; //방어력
    int HitD; //받는 데미지


    public static Transform target;

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
    }

    public void GunnerHitFunc(int damage)
    {
        HitD = damage - GunnerD;
        if (HitD >= 1)
        {
            GunnerHP = GunnerHP - (float)HitD;
            Debug.Log(HitD);
        }
        else if (HitD < 1)
        {
            GunnerHP = GunnerHP - 1f;
            Debug.Log(HitD);
        }
    }

}
