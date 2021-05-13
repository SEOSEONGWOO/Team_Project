using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    public Animator avatar;

    public Transform SlimeLo;   // 스켈레톤 위치

    public Vector3 SlimeVec; // 스켈레톤 위치 vector값

    private NavMeshAgent nav;

    public GameObject Gobj; // 따라갈 캐릭터 게임오브젝트 넣어줄 빈 값

    float distance; // 스켈레톤 , 플레이어 거리 넣어 줄 float

    bool SlimeDie = false; // 죽음 판단

    bool SlimeAttack = false; // 스켈레톤 공격 판단용

    /*    float SlimeSkill1Cool = 0.0f;

        bool SlimeSkill1CoolOn = false;*/

    float SlimeSkill2 = 0.0f;

    float SlimeSkill2Cool = 5f;

    bool SlimeSkill2CoolOn = false;

    float AttackDel = 0f;

    float SlimeAttackDel = 2f;

    int SlimeHP = 200;

    static public int SlimeDamage = 20;

    static public int AttackMotion = 5; // 공격 상태 판단

    bool PlayerCheck = false;

    void Start()
    {
        //AwakeT += Time.deltaTime;
        Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        nav = GetComponent<NavMeshAgent>();

        //Debug.Log(AttackMotion);

        //Debug.Log(AttackDel);

        SlimeVec = SlimeLo.transform.position; // 스켈레톤 현재 위치값 

        distance = Vector3.Distance(SlimeVec, Player_Scr.CLC);

        if (SlimeHP <= 0 && SlimeDie == false)
        {
            SDie();
        }

        else if (SlimeDie == false)
        {
            if (AttackMotion == 5)
            {
                if (distance < 30f)
                {
                    AttackMotion = 0;
                }
            }
            else if (AttackMotion != 5)
            {
                nav.SetDestination(Player_Scr.CLC);

                transform.LookAt(Player_Scr.CLC);
            }

            if (distance <= 2.5f)  // 거리 2.5f 보다 가까울 때
            {
                nav.speed = 0; // 속도 0 
                SlimeAttack = true; // 공격상태로 바꿔 줌
            }
            else if (distance > 2.5f) // 거리 2.5f 보다 멀 면
            {
                SlimeAttack = false; // 공격상태 끄고
                nav.speed = 4; // 속도 4
            }

            if (SlimeSkill2CoolOn == true)
            {
                SlimeSkill2 += Time.deltaTime;

                if (SlimeSkill2 >= SlimeSkill2Cool)
                {
                    SlimeSkill2 = 0.0f;
                    SlimeSkill2CoolOn = false;
                }
            }

            if (SlimeAttack == true) // 공격 상태일 때
            {
                avatar.SetBool("FollowFollowMe", false); // 이동하는 애니메이션 상태 끄고
                if (AttackMotion == 0) // 공격 모드 0 일 때
                {
                    AttackMotion = Random.Range(1, 3); // 공격모드 1 , 2 랜덤으로 돌려 줌
                }
                else if (AttackMotion == 1) // 공격모드 1이면
                {
                    avatar.SetTrigger("Attack01"); // Attack01 실행
                    AttackMotion = 3; // 대기상태로 변경
                }
                else if (AttackMotion == 2) // 공격모드 2고
                {
                    if (SlimeSkill2CoolOn == false) // 공격모드2 쿨타임이 없을 때
                    {
                        avatar.SetTrigger("Attack02"); // Attack02 실행
                        AttackMotion = 3; // 대기상태로 변경
                        SlimeSkill2CoolOn = true; // 스킬 2 쿨타임 돌려줌.
                    }
                    else if (SlimeSkill2CoolOn == true) // 쿨타임이면
                    {
                        AttackMotion = 3; // 대기상태로 변경
                    }
                }
                else if (AttackMotion == 3) // 대기상태면 
                {
                    AttackDel += Time.deltaTime; // 딜레이 시간 될 때 까지 증가
                    if (AttackDel >= SlimeAttackDel) // 딜레이 시간 지나면
                    {
                        AttackMotion = 0; // 다시 랜덤모드
                        AttackDel = 0f; // 딜레이 초기화
                    }
                }
            }
            else if (SlimeAttack == false) // 공격모드 꺼지면
            {
                avatar.SetBool("FollowFollowMe", true);
            }
        }
    }

    void SDie()
    {
        nav.speed = 0;
        avatar.SetTrigger("DIE");
        SlimeDie = true;
    }

    public void SetDamageAI()
    {
        if (AttackMotion == 5)
        {
            AttackMotion = 0;
        }
        SlimeHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(SlimeHP);
    }
}

