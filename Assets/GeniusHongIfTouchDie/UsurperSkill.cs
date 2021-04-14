using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsurperSkill : MonoBehaviour
{
    public Animator avatar;

    int DrgHP = 100000; // HP
    int Sk0Da = 30;        // Sk0 데미지
    int Sk1Da = 15;        // Sk1 데미지
    int Sk2Da = 10;        // Sk2 데미지
    int Sk3Da = 5;          // Sk3 데미지

    float DrgAtIng = 0.0f;  // 공격 모션 시간주기용.
    float DrgAtIngEnd = 0.0f;   // 공격 모션 끝나는 시간 체크용

    float Sk0De = 3f;        // Sk0 쿨타임
    float Sk1De = 10f;      // Sk1 쿨타임
    float Sk2De = 30f;      // Sk2 쿨타임
    float Sk3De = 60f;      // Sk3 쿨타임

    float Sk0C = 0.0f;       // Sk0 쿨타임 돌리는 용도
    float Sk1C = 0.0f;       // Sk1 쿨타임 돌리는 용도
    float Sk2C = 0.0f;       // Sk2 쿨타임 돌리는 용도
    float Sk3C = 0.0f;       // Sk3 쿨타임 돌리는 용도

    int SkillC = 7; // 0 ~ 3 스킬 4 스턴 5 Idle 6 죽음 7 자는 모습 8 쿨타임 돌리는 중

    float StunT = 0.0f;
    float AwakeT = 0.0f;

    int[] Skills = new int[3];

    void Start()
    {
        avatar.SetTrigger("Awake");
        SkillC = 5;
        //AwakeT += Time.deltaTime;
    }

    void Update()
    {
        // Sk6 이 아닐 때(죽음상태 아닐 때) {}

        // 랜덤으로 SkillC 에 Skills[0.1.2.3]중 하나 줌.

        // SkillC(숫자) = Sk(숫자) Trigger 실행 ( 쿨타임일 시 다른 번호 실행 )

        // 실행되는 애니메이션 시간만큼 일시정지.

        // 끝나면 Sk5(Idle) 대기 및 쿨타임 시작.

        // 스턴스킬 맞으면 무조건 Sk5 실행.

        /*        if (AwakeT == 3.333f)
                {
                    SkillC = 5;
                }*/
        Debug.Log(SkillC);
        Debug.Log(DrgAtIng);
        if (DrgHP <= 0)
        {
            SkillC = 6;
        }

        if(SkillC == 4)
        {
            StunT += Time.deltaTime;
            if (StunT == 2.0f)
            {
                SkillC = 5;
                StunT = 0.0f;
            }
        }

        if(SkillC == 8)
        {
            if(DrgAtIng == DrgAtIngEnd)
            {
                SkillC = 5;
                DrgAtIng = 0.0f;
                DrgAtIngEnd = 0.0f;
            }
        }

        if(SkillC == 5)
        {
            SkillC = Random.Range(0, 3);

            if(SkillC == 0 && Sk0C == 0.0f)     // 쿨설정 . 쿨상태로 애니메이션상태(8)로 돌리고 쿨되면 다시 5 괜찮은데?
            {
                    avatar.SetTrigger("Sk0");
                    DrgAtIng += Time.deltaTime;
                    DrgAtIngEnd = 1.1f;
                    Sk0C += Time.deltaTime;
                    SkillC = 8;
            }
            else if(SkillC == 1 && Sk1C == 0.0f)
            {
                avatar.SetTrigger("Sk1");
                DrgAtIng += Time.deltaTime;
                if (DrgAtIng == 3.1f)
                {
                    SkillC = 5;
                    Sk1C += Time.deltaTime;
                    DrgAtIng = 0.0f;
                }
            }
            else if(SkillC == 2 && Sk2C == 0.0f)
            {
                avatar.SetTrigger("Sk2");
                DrgAtIng += Time.deltaTime;
                if (DrgAtIng == 2.3f)
                {
                    SkillC = 5;
                    Sk2C += Time.deltaTime;
                    DrgAtIng = 0.0f;
                }
            }
            else if(SkillC == 3 && Sk3C == 0.0f)
            {
                avatar.SetTrigger("Sk3");
                DrgAtIng += Time.deltaTime;
                if (DrgAtIng == 14.1f)
                {
                    SkillC = 5;
                    Sk3C += Time.deltaTime;
                    DrgAtIng = 0.0f;
                }
            }

            if(Sk0C == Sk0De)
            {
                Sk0C = 0.0f;
            }
            if (Sk1C == Sk1De)
            {
                Sk1C = 0.0f;
            }
            if (Sk2C == Sk2De)
            {
                Sk2C = 0.0f;
            }
            if (Sk3C == Sk3De)
            {
                Sk3C = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider Stun)
    {
        avatar.SetTrigger("Stun");
        SkillC = 4;
    }
}
