using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KissSkeleton : MonoBehaviour
{
    public Animator avatar;

    public AudioClip audioAttack1;
    public AudioClip audioAttack2;
    public AudioClip audioHit;
    public AudioClip audioDie;

    AudioSource audioSource;

    public Transform SkeletonLo;   // 스켈레톤 위치

    public Vector3 SkeletonVec; // 스켈레톤 위치 vector값

    private NavMeshAgent nav;

    public GameObject Gobj; // 따라갈 캐릭터 게임오브젝트 넣어줄 빈 값

    float distance; // 스켈레톤 , 플레이어 거리 넣어 줄 float

    bool SkeletonDie = false; // 죽음 판단

    bool SkeletonAttack = false; // 스켈레톤 공격 판단용

    /*    float SkeletonSkill1Cool = 0.0f;

        bool SkeletonSkill1CoolOn = false;*/

    float SkeletonSkill2 = 0.0f;

    float SkeletonSkill2Cool = 5f;

    bool SkeletonSkill2CoolOn = false;

    float AttackDel = 0f;

    float SkeletonAttackDel = 2f;

    int SkeletonHP = 100;

    static public int SkeletonDamage = 20;

    static public int AttackMotion = 5; // 공격 상태 판단

    bool PlayerCheck = false;

    void Start()
    {
        //AwakeT += Time.deltaTime;
        //Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        this.audioSource = GetComponent<AudioSource>();

        Gobj = GameObject.Find("HPCharacter");

        nav = GetComponent<NavMeshAgent>();

        //Debug.Log(AttackMotion);

        //Debug.Log(AttackDel);

        SkeletonVec = SkeletonLo.transform.position; // 스켈레톤 현재 위치값 

        distance = Vector3.Distance(SkeletonVec, Player_Scr.CLC);

        if (SkeletonHP <= 0 && SkeletonDie == false)
        {
            SDie();
        }

        else if (SkeletonDie == false)
        {
            if (AttackMotion == 5)
            {
                if (distance < 20f)
                {
                    AttackMotion = 0;
                }
            }
            else if (AttackMotion != 5 && distance < 20.0f)
            {
                nav.SetDestination(Player_Scr.CLC);

                transform.LookAt(Player_Scr.CLC);
            }

            if (distance <= 2.5f)  // 거리 2.5f 보다 가까울 때
            {
                nav.speed = 0; // 속도 0 
                SkeletonAttack = true; // 공격상태로 바꿔 줌
            }
            else if (distance > 2.5f && distance < 20.0f) // 거리 2.5f 보다 멀 면
            {
                SkeletonAttack = false; // 공격상태 끄고
                nav.speed = 2; // 속도 4
            }
            else if (distance > 20.0f)
            {
                avatar.SetBool("FollowFollowMe", false);
                nav.speed = 0;
            }

            if (SkeletonSkill2CoolOn == true)
            {
                SkeletonSkill2 += Time.deltaTime;

                if (SkeletonSkill2 >= SkeletonSkill2Cool)
                {
                    SkeletonSkill2 = 0.0f;
                    SkeletonSkill2CoolOn = false;
                }
            }

            if (SkeletonAttack == true) // 공격 상태일 때
            {
                avatar.SetBool("FollowFollowMe", false); // 이동하는 애니메이션 상태 끄고
                if (AttackMotion == 0) // 공격 모드 0 일 때
                {
                    AttackMotion = Random.Range(1, 3); // 공격모드 1 , 2 랜덤으로 돌려 줌
                }
                else if (AttackMotion == 1) // 공격모드 1이면
                {
                    avatar.SetTrigger("Attack01"); // Attack01 실행
                    StartCoroutine(PlaySound("Attack1"));
                    AttackMotion = 3; // 대기상태로 변경
                }
                else if (AttackMotion == 2) // 공격모드 2고
                {
                    if (SkeletonSkill2CoolOn == false) // 공격모드2 쿨타임이 없을 때
                    {
                        avatar.SetTrigger("Attack02"); // Attack02 실행
                        StartCoroutine(PlaySound("Attack2"));
                        AttackMotion = 3; // 대기상태로 변경
                        SkeletonSkill2CoolOn = true; // 스킬 2 쿨타임 돌려줌.
                    }
                    else if (SkeletonSkill2CoolOn == true) // 쿨타임이면
                    {
                        AttackMotion = 3; // 대기상태로 변경
                    }
                }
                else if (AttackMotion == 3) // 대기상태면 
                {
                    AttackDel += Time.deltaTime; // 딜레이 시간 될 때 까지 증가
                    if (AttackDel >= SkeletonAttackDel) // 딜레이 시간 지나면
                    {
                        AttackMotion = 0; // 다시 랜덤모드
                        AttackDel = 0f; // 딜레이 초기화
                    }
                }
            }
            else if (SkeletonAttack == false && distance < 20.0f) // 공격모드 꺼지면
            {
                avatar.SetBool("FollowFollowMe", true);
            }
        }
    }

    void SDie()
    {
        ZombieSpawn.MonsterStack += 1;
        nav.speed = 0;
        StartCoroutine(PlaySound("Die"));
        avatar.SetTrigger("DIE");
        SkeletonDie = true;
        Destroy(gameObject, 2.0f);
    }

    public void SetDamageAI()
    {
        if (AttackMotion == 5)
        {
            AttackMotion = 0;
        }
        SkeletonHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(SkeletonHP);
        StartCoroutine(PlaySound("Hit"));
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Skill1")
        {
            SkeletonHP -= 500;
        }

        if (other.tag == "skill1_2")
        {
            SkeletonHP -= 500;

        }
        if (other.tag == "Skill1_4")
        {
            SkeletonHP -= 500;
        }

        if (other.tag == "Skill2_1")
        {
            SkeletonHP -= 500;
        }

        if (other.tag == "Skill2_3")
        {
            SkeletonHP -= 500;

        }
        if (other.tag == "Skill2_4")
        {
            SkeletonHP -= 500;

        }
        if (other.tag == "Skill3_1")
        {
            SkeletonHP -= 500;

        }
        if (other.tag == "Skill3_2")
        {
            SkeletonHP -= 500;
        }
    }

    IEnumerator PlaySound(string action)
    {
        switch (action)
        {
            case "Attack1":
                yield return new WaitForSeconds(0.5f);
                audioSource.clip = audioAttack1;
                break;
            case "Attack2":
                yield return new WaitForSeconds(0.8f);
                audioSource.clip = audioAttack2;
                break;
            case "Hit":
                audioSource.clip = audioHit;
                break;
            case "Die":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }
}

