using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UsurperSkill : MonoBehaviour
{
    public Animator avatar;
    public GameObject Gobj;
    private NavMeshAgent nav;

    static public int DrgHP = 100000; // HP
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

    bool DragonDie = false;

    bool DragonAR = false;

    public Transform DrgL; // 드래곤 위치

    public Vector3 DrgLN; // 드래곤 위치 vector값

    float distance; // 드래곤 , 플레이어 거리 넣어 줄 float

    static public bool FireOn = false;

    static public bool AttackOn = false;

    void Start()
    {
        //AwakeT += Time.deltaTime;
        Gobj = GameObject.Find("HPCharacter");
    }

    void Update()
    {
        
        nav = GetComponent<NavMeshAgent>();

        DrgLN = DrgL.transform.position; // DrgLN 에 드래곤 위치값 넣어줌

        distance = Vector3.Distance(DrgLN, Player_Scr.CLC); // distance에 드래곤 , 플레이어 거리 비교

        if (DrgHP <= 0 && DragonDie == false) // 만약 피가 0이하고 죽음 체크 안되면
        {
            DragonDie = true; // 죽음 체크
            SkillC = 6; // 죽음상태로 변경
            avatar.SetTrigger("Dies"); // Dies 모션 
        }

        if (SkillC != 6) // 죽음 상태 아닐 때
        {
            Debug.Log(SkillC); // 지속 적으로 드래곤 상태 체크

            if (SkillC == 7)
            {
                gameObject.GetComponent<SphereCollider>().enabled = true;
                nav.speed = 0;
            }
            else if (SkillC != 7)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
                transform.LookAt(Player_Scr.CLC);
                if (distance <= 15.0f) // 만약 거리가 15이하이면 스킬 돌림
                {
                    DragonAR = true;
                }
                else if (distance > 15.0f)
                {
                    DragonAR = false;
                }
            }

            if (SkillC == 10) // 10 = 시작 ( 거리이내로 플레이어가 들어오면)
            {
                AwakeT += Time.deltaTime; // 시작모션 대기시간 돌려주고 
                if (AwakeT >= AwakeD) // 모션 끝나는 시간쯤 되면
                {
                    AwakeT = 0.0f; 
                    SkillC = 5; // 대기상태로 변경
                }
            }

            if (Sk1Del == 1)  // 스킬 1 쿨타임이 켜지면
            {
                Sk1C += Time.deltaTime; // 스킬 1 쿨타임 돌아감
                if (Sk1C >= Sk1De) // 쿨타임 다 되면
                {
                    Sk1C = 0.0f; // 쿨초기화
                    Sk1Del = 0; // 쿨타임 off
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

            if (DragonAR == true)
            {
                avatar.SetBool("FollowFollowME", false);
                nav.speed = 0;
                if (SkillC == 1) // 스킬 1 상태일 때 
                {
                    if (Sk1Del == 0) // 스킬 1 쿨타임이 아니면 
                    {
                        StartCoroutine(AttackDelay(5));

                        avatar.SetTrigger("Sk1"); // 스킬1 사용
                        Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Sk1Da); // 공격

                        DrgAtIngEnd = 5.1f; // 모션시간
                        Sk1Del = 1; // 스킬 쿨타임
                        SkillC = 8; // 스킬사용중으로 변경
                    }
                    else
                    {
                        SkillC = 5; // 만약 스킬 쿨타임이면 다시 대기상태로 변경
                    }
                }

                if (SkillC == 2)
                {
                    if (Sk2Del == 0)
                    {
                        StartCoroutine(AttackDelay(7));

                        avatar.SetTrigger("Sk2");
                        
                        Gobj.GetComponent<Player_Scr>().GunnerHitFunc(Sk2Da);
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
                        StartCoroutine(FireDelay(3));

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

                if (SkillC == 5) // 대기 상태면
                {
                    SkillC = Random.Range(1, 5); // 1~4 스킬 사용
                }
            }
            else if(DragonAR == false && SkillC != 7)
            {
                avatar.SetBool("FollowFollowME", true);
                nav.speed = 4;
                nav.SetDestination(Player_Scr.CLC);
            }

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




            if (SkillC == 9) // 스턴 스킬 맞으면
            {
                StunT += Time.deltaTime;
                if (StunT >= 2.0f) // 2초간 스턴
                {
                    SkillC = 5;
                    StunT = 0.0f;
                }
            }

            if (SkillC == 8) // 스킬 돌아가는 중이면
            {
                nav.speed = 0;
                DrgAtIng += Time.deltaTime; // 스킬대기시간
                if (DrgAtIng >= DrgAtIngEnd)
                {
                    DrgAtIng = 0.0f;
                    DrgAtIngEnd = 0.0f;
                    SkillC = 5;
                }
            }
        }
        else if(SkillC == 6)
        {

        }
    }

    private void OnCollisionEnter(Collision Player)
    {
        avatar.SetTrigger("Awake");
        SkillC = 10;
    }

/*    private void OnTriggerEnter(Collider Stun)
    {
        avatar.SetTrigger("Stun");
        SkillC = 9;
    }*/

    public void SetDamageAI()
    {
        if(SkillC == 7)
        {
            avatar.SetTrigger("Awake");
            SkillC = 10;
        }
        DrgHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(DrgHP);
    }

    public IEnumerator AttackDelay(float FT)
    {
        AttackOn = true;

        yield return new WaitForSeconds(FT);

        AttackOn = false;
    }

    public IEnumerator FireDelay(float FT)
    {
        FireOn = true;

        yield return new WaitForSeconds(FT);

        FireOn = false;
    }
}

