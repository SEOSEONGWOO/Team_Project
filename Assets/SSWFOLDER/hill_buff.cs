using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hill_buff : MonoBehaviour
{
    public static bool a = false;

    private void Update()
    {
        if(a == true)
        {
            HpUp();
        }
    }


     void OnTriggerStay(Collider other)  //버프안에 플레이어가 있으면
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("플레이어 있니?");
            a = true;
        }
    }

    void HpUp()
    {
        if (Player_Scr.HP != Player_Scr.maxHP)
        {
            //마나는 초당 0.5씩 회복
            Player_Scr.HP += Time.deltaTime * 10f;

            //마나를 회복했는데 최대 마나보다 크다면 현재마나를 최대마나량으로 변경
            if (Player_Scr.HP > Player_Scr.maxHP)
            {
                Player_Scr.HP = Player_Scr.maxHP;
            }
            //회복된 마나량을 Mpbar슬라이더에 업데이트
        }
    }
}
