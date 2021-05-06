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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            //충돌시 hp -20
            currHp -= 20.0f;
            //hpbar에 적용
            hpBar.fillAmount = currHp / initHp;
        }
        
    }
}
