using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EnemyHpCs : MonoBehaviour
{
    private float initHp = 100.0f;
    private float currHp = 100.0f;
    public Image hpBar;
    public static bool ishit = false;

    // Update is called once per frame
    void Update()
    {
        if(Bullet_test.hitname == "Sphere" && Bullet_test.isEnemy == true)
        {
            //총알이랑 충돌 발생시
            
            Debug.Log("Bullet 충돌 HP -20");
            //충돌시 hp -20
            currHp -= 20.0f;
            //hpbar에 적용
            hpBar.fillAmount = currHp / initHp;

            Bullet_test.isEnemy = false;
        }
    }
}
