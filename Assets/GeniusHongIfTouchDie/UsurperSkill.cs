using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsurperSkill : MonoBehaviour
{
    public Animator avatar;

    static int DrgHP = 100000; // HP
    int Sk1Da = 30;        // Sk0 데미지
    int Sk2Da = 15;        // Sk1 데미지
    int Sk3Da = 10;        // Sk2 데미지
    int Sk4Da = 5;          // Sk3 데미지

    float DrgAtIng = 0.0f;  // 공격 모션 시간주기용.
    float DrgAtIngEnd = 0.0f;   // 공격 모션 끝나는 시간 체크용

    float Sk1De = 3f;        // Sk1 쿨타임
    float Sk2De = 10f;      // Sk2 쿨타임
    float Sk3De = 30f;      // Sk3 쿨타임
    float Sk4De = 60f;      // Sk4 쿨타임

    int Sk1Del = 0;
    int Sk2Del = 0;
    int Sk3Del = 0;
    int Sk4Del = 0;

    float Sk1C = 0.0f;       // Sk1 쿨타임 돌리는 용도
    float Sk2C = 0.0f;       // Sk2 쿨타임 돌리는 용도
    float Sk3C = 0.0f;       // Sk3 쿨타임 돌리는 용도
    float Sk4C = 0.0f;       // Sk4 쿨타임 돌리는 용도

    int SkillC = 7; // 1 ~ 4 스킬 5 Idle 6 죽음 7 자는 모습 8 쿨타임 돌리는 중 9 스턴 10 시작체크

    float StunT = 0.0f;
    float AwakeT = 0.0f;
    float AwakeD = 6.0f;

    int[] Skills = new int[3];

    void Start()
    {
        avatar.SetTrigger("Awake");
        SkillC = 10;
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

        if(SkillC == 10)
        {
            AwakeT += Time.deltaTime;
            if(AwakeT >= AwakeD)
            {
                AwakeT = 0.0f;
                SkillC = 5;
            }
        }
        Debug.Log(SkillC);
        if (DrgHP <= 0)
        {
            SkillC = 6;
        }

        if(Sk1Del == 1)
        {
            Sk1C += Time.deltaTime;
            if (Sk1C >= Sk1De)
            {
                Sk1C = 0.0f;
                Sk1Del = 0;
            }
        }
        if (Sk2Del == 1)
        {
            Sk2C += Time.deltaTime;
            if (Sk2C >= Sk2De)
            {
                Sk2C = 0.0f;
                Sk2Del = 0;
            }
        }
        if (Sk3Del == 1)
        {
            Sk3C += Time.deltaTime;
            if (Sk3C >= Sk3De)
            {
                Sk3C = 0.0f;
                Sk3Del = 0;
            }
        }
        if (Sk4Del == 1)
        {
            Sk4C += Time.deltaTime;
            if (Sk4C >= Sk4De)
            {
                Sk4C = 0.0f;
                Sk4Del = 0;
            }
        }

        if (SkillC == 9)
        {
            StunT += Time.deltaTime;
            if (StunT >= 2.0f)
            {
                SkillC = 5;
                StunT = 0.0f;
            }
        }

        if(SkillC == 8)
        {
            DrgAtIng += Time.deltaTime;
            if (DrgAtIng >= DrgAtIngEnd)
            {
                DrgAtIng = 0.0f;
                DrgAtIngEnd = 0.0f;
                SkillC = 5;
            }
        }

        if(SkillC == 1)
        {
            if(Sk1Del == 0)
            {
                avatar.SetTrigger("Sk1");
                DrgAtIngEnd = 5.1f;
                Sk1Del = 1;
                SkillC = 8;
            }
            else
            {
                SkillC = 5;
            }
        }

        if (SkillC == 2)
        {
            if (Sk2Del == 0)
            {
                avatar.SetTrigger("Sk2");
                DrgAtIngEnd = 7.1f;
                Sk2Del = 1;
                SkillC = 8;
            }
            else
            {
                SkillC = 5;
            }
        }

        if (SkillC == 3)
        {
            if (Sk3Del == 0)
            {
                avatar.SetTrigger("Sk3");
                DrgAtIngEnd = 6.3f;
                Sk3Del = 1;
                SkillC = 8;
            }
            else
            {
                SkillC = 5;
            }
        }
        if (SkillC == 4)
        {
            if (Sk4Del == 0)
            {
                avatar.SetTrigger("Sk4");
                DrgAtIngEnd = 17.1f;
                Sk4Del = 1;
                SkillC = 8;
            }
            else
            {
                SkillC = 5;
            }
        }

        if (SkillC == 5)
        {
            SkillC = Random.Range(1, 5);

            Debug.Log(SkillC);
        }
    }

    private void OnTriggerEnter(Collider Stun)
    {
        avatar.SetTrigger("Stun");
        SkillC = 9;
    }

    public void SetDamageAI()
    {
        DrgHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(DrgHP);
    }
}
