using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsurperSkill : MonoBehaviour
{
    int DrgHP = 100000;
    int Sk1Da = 30;
    int Sk2Da = 15;
    int Sk3Da = 10;
    int Sk4Da = 5;

    float Sk1De = 60f;
    float Sk2De = 30f;
    float Sk3De = 10f;
    float Sk4De = 3f;

    int SkillC = 7; // 0 ~ 3 스킬 4 스턴 5 Idle 6 죽음 7 자는 모습

    int[] Skills = new int[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sk6 이 아닐 때(죽음상태 아닐 때) {}

        // 랜덤으로 SkillC 에 Skills[0.1.2.3]중 하나 줌.

        // SkillC(숫자) = Sk(숫자) Trigger 실행 ( 쿨타임일 시 다른 번호 실행 )

        // 실행되는 애니메이션 시간만큼 일시정지.

        // 끝나면 Sk5(Idle) 대기 및 쿨타임 시작.

        // 스턴스킬 맞으면 무조건 Sk5 실행.
    }
}
