using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HpCs : MonoBehaviourPun
{
    private float initHp = 100.0f;
    private float currHp = 100.0f;
    public Image hpBar;
    public static bool ishit = false;
    void Update()
    {
        //Debug.Log(PhotonNetwork.NickName+"/"+hpBar.fillAmount);
    }

    void OnCollisionEnter(Collision collision)
    {
        //자신의 플레이어
        if (photonView.IsMine)
        {
            //몬스터랑 충돌 발생시
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("Enemy 충돌 HP -20");
                //충돌시 hp -20
                currHp -= 20.0f;
                //hpbar에 적용
                hpBar.fillAmount = currHp / initHp;
            }
        }
        

    }
}
