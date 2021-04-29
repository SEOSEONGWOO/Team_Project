using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonScript : MonoBehaviour
{
    public Animator avatar;

    public GameObject Gobj; // 쫓아갈 플레이어

    private NavMeshAgent nav;

    public GameObject Coin; // 죽으면 떨어뜨릴 코인(넣어줘야함)

    public Transform SkeletonLo; // 골렘 위치

    public Vector3 SkeletonVec; // 골렘 벡터값

    float distance; // 골렘과 플레이어 거리계산용

    bool SkeletonDie = false; // 골렘 죽은지 안죽은지 판단용.

    bool SkeletonCheck = false; // 골렘 플레이어 감지상태 판단용

    bool SkeletonAttack = false; // 골렘 공격 판단용

    float SkeletonSkill1Cool = 0.0f;

    bool SkeletonSkill1CoolOn = false;

    float SkeletonSkill2Cool = 0.0f;

    bool SkeletonSkill2CoolOn = false;

    float AttackDel = 0f;

    float SkeletonAttackDel = 0f;

    int SkeletonHP = 500;

    static public int SkeletonDamage = 20;

    int AttackMotion = 0;

    static public int SkillOn = 0;

    void Start()
    {
        Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        Debug.Log(AttackMotion);

        nav = GetComponent<NavMeshAgent>();

        SkeletonVec = SkeletonLo.transform.position; // 골렘 현재 위치값 

        distance = Vector3.Distance(SkeletonVec, Player_Scr.CLC); // 플레이어와 거리 계산.

        Quaternion rot; // 회전값
        rot = Quaternion.Euler(Random.Range(0, 0), Random.Range(0, 0), Random.Range(0, 0)); // 랜덤 회전 값
        if(SkeletonHP <= 0 && SkeletonDie == false)
        {
            SkeletonDie = true;
        }

        if(SkeletonSkill2CoolOn == true)
        {
            SkeletonSkill2Cool += Time.deltaTime;
            if(SkeletonSkill2Cool >= 5.0f)
            {
                SkeletonSkill2CoolOn = false;
                SkeletonSkill2Cool = 0.0f;
            }
        }

        if (SkeletonDie == false)
        {
            if(SkeletonCheck == true)
            {
                nav.SetDestination(Player_Scr.CLC);
                transform.LookAt(Player_Scr.CLC);

                if(distance <= 2.5f)
                {
                    nav.speed = 0;
                    SkeletonAttack = true;
                }
                else if(distance > 2.5f)
                {
                    SkeletonAttack = false;
                    nav.speed = 4;
                }

                if(SkeletonAttack == true)
                {
                    avatar.SetBool("FollowFollowMe", false);
                    if(AttackMotion == 0)
                    {
                        AttackMotion = Random.Range(1, 3);
                    }
                    else if(AttackMotion == 1)
                    {
                        StartCoroutine(SkeletonSkills(AttackMotion));
                        SkeletonAttackDel = 0.5f;
                        AttackMotion = 3;
                    }
                    else if(AttackMotion == 2)
                    {
                        if(SkeletonSkill2CoolOn == false)
                        {
                            StartCoroutine(SkeletonSkills(AttackMotion));
                            SkeletonAttackDel = 1f;
                            AttackMotion = 3;
                        }
                        else if(SkeletonSkill2CoolOn = true)
                        {
                            AttackMotion = 3;
                        }
                    }
                    else if(AttackMotion == 3)
                    {
                        AttackDel += Time.deltaTime;
                        if(AttackDel >= SkeletonAttackDel)
                        {
                            AttackMotion = 0;
                            AttackDel = 0f;
                        }
                    }

                }
                else if(SkeletonAttack == false)
                {
                    AttackMotion = 0;
                    avatar.SetBool("FollowFollowMe", true);
                }
            }
            else if(SkeletonCheck == false)
            {
                if(distance <= 30f)
                {
                    SkeletonCheck = true;
                }
            }
        }
        else if(SkeletonDie == true)
        {
            avatar.SetBool("Die", true);
            StartCoroutine(SkeletonDead());
        }
    }

    public void SetDamageAI()
    {
        if(SkeletonCheck == false)
        {
            SkeletonCheck = true;
        }
        SkeletonHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
    }

    public IEnumerator SkeletonSkills(int AttackMotion)
    {
        if(AttackMotion == 1)
        {
            avatar.SetTrigger("Attack1");

            SkillOn = AttackMotion;

            yield return new WaitForSeconds(0.5f);

            SkillOn = 0;
        }
        else if(AttackMotion == 2)
        {
            avatar.SetTrigger("Attack2");

            SkillOn = AttackMotion;

            yield return new WaitForSeconds(1);

            SkeletonSkill2CoolOn = true;

            SkillOn = 0;
        }
    }

    public IEnumerator SkeletonDead()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        Instantiate(Coin, SkeletonVec, Quaternion.identity);

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
