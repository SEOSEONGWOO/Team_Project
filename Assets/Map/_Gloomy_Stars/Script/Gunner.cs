using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Gunner : MonoBehaviour
{
    public Animator avatar;

    float GunnerHP = 100f;  // 체력
    int GunnerA = 10; //공격력
    int GunnerD = 5; //방어력
    int HitD; //받는 데미지


    //public static Transform target; // 

    public Transform clcl;  // 현재 캐릭터 위치 값( 따라오게 하기 위함 )

    public Text GunnerHPText; 
    public Slider GunnerHPBar;

    public static int GunnerMoney;  // 돈

    public GameObject GunnerMoneyText; 

    public static Vector3 CLC;  // CLC 현재위치 (팔로팔로미에 보내줄 vector 배열)


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

        GunnerHPText.text = "Health: " + GunnerHP.ToString("0");
    }

    public void GunnerHitFunc(int damage)   // 
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
