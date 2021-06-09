using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialOrc : MonoBehaviour
{
    public Animator avatar;

    public Transform OrcLo;   // 스켈레톤 위치

    public Vector3 OrcVec; // 스켈레톤 위치 vector값

    private NavMeshAgent nav;

    public GameObject Gobj; // 따라갈 캐릭터 게임오브젝트 넣어줄 빈 값

    public GameObject SpCoin; // 죽으면 떨어뜨릴 코인

    public GameObject TutorialBox;

    float distance; // 스켈레톤 , 플레이어 거리 넣어 줄 float

    bool OrcDie = false; // 죽음 판단

    bool OrcAttack = false; // 스켈레톤 공격 판단용

    /*    float OrcSkill1Cool = 0.0f;

        bool OrcSkill1CoolOn = false;*/

    float OrcSkill2 = 0.0f;

    float OrcSkill2Cool = 5f;

    bool OrcSkill2CoolOn = false;

    float AttackDel = 0f;

    float OrcAttackDel = 2f;

    int OrcHP = 250;

    static public int OrcDamage = 20;

    static public int AttackMotion = 5; // 공격 상태 판단

    bool PlayerCheck = false;

    public AudioClip audioAttack1;
    public AudioClip audioAttack2;
    public AudioClip audioHit;
    public AudioClip audioDie;

    AudioSource audioSource;

    void Start()
    {
        //AwakeT += Time.deltaTime;
        //Gobj = GameObject.FindWithTag("Player");
        //Gobj2 = GameObject.FindWithTag("Player2");
        // 두마리일 때 위에 꺼 한번 더쓰고 거리계산해서 짧은거 먼저 쫓게 만들어라 알겠지 ? 
    }

    void Update()
    {
        this.audioSource = GetComponent<AudioSource>();

        Gobj = GameObject.FindWithTag("Player");

        nav = GetComponent<NavMeshAgent>();

        //Debug.Log(AttackMotion);

        //Debug.Log(AttackDel);

        OrcVec = OrcLo.transform.position; // 스켈레톤 현재 위치값 

        distance = Vector3.Distance(OrcVec, Player_Scr.CLC);

        if (OrcHP <= 0 && OrcDie == false)
        {
            SDie();
        }

        else if (OrcDie == false)
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
                OrcAttack = true; // 공격상태로 바꿔 줌
            }
            else if (distance > 2.5f && distance < 20.0f) // 거리 2.5f 보다 멀 면
            {
                OrcAttack = false; // 공격상태 끄고
                nav.speed = 2; // 속도 4
            }
            else if (distance > 20.0f)
            {
                avatar.SetBool("FollowFollowMe", false);
                nav.speed = 0;
            }

            if (OrcSkill2CoolOn == true)
            {
                OrcSkill2 += Time.deltaTime;
                StartCoroutine(PlaySound("Attack2"));

                if (OrcSkill2 >= OrcSkill2Cool)
                {
                    OrcSkill2 = 0.0f;
                    OrcSkill2CoolOn = false;
                }
            }

            if (OrcAttack == true) // 공격 상태일 때
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
                    if (OrcSkill2CoolOn == false) // 공격모드2 쿨타임이 없을 때
                    {
                        avatar.SetTrigger("Attack02"); // Attack02 실행
                        AttackMotion = 3; // 대기상태로 변경
                        OrcSkill2CoolOn = true; // 스킬 2 쿨타임 돌려줌.
                    }
                    else if (OrcSkill2CoolOn == true) // 쿨타임이면
                    {
                        AttackMotion = 3; // 대기상태로 변경
                    }
                }
                else if (AttackMotion == 3) // 대기상태면 
                {
                    AttackDel += Time.deltaTime; // 딜레이 시간 될 때 까지 증가
                    if (AttackDel >= OrcAttackDel) // 딜레이 시간 지나면
                    {
                        AttackMotion = 0; // 다시 랜덤모드
                        AttackDel = 0f; // 딜레이 초기화
                    }
                }
            }
            else if (OrcAttack == false && distance < 20.0f) // 공격모드 꺼지면
            {
                avatar.SetBool("FollowFollowMe", true);
            }
        }
    }

    void SDie()
    {
        nav.speed = 0;
        avatar.SetTrigger("DIE");
        StartCoroutine(PlaySound("Die"));
        OrcDie = true;
        Instantiate(SpCoin, new Vector3(OrcVec.x, OrcVec.y - 1, OrcVec.z), Quaternion.identity);
        TutorialBox.SetActive(true);
        Destroy(gameObject, 2.0f);
    }

    public void SetDamageAI()
    {
        if (AttackMotion == 5)
        {
            AttackMotion = 0;
        }
        OrcHP -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        Debug.Log(OrcHP);
        StartCoroutine(PlaySound("Hit"));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Skill1")
        {
            OrcHP -= 500;
        }

        if (other.tag == "skill1_2")
        {
            OrcHP -= 500;

        }
        if (other.tag == "Skill1_4")
        {
            OrcHP -= 500;
        }

        if (other.tag == "Skill2_1")
        {
            OrcHP -= 500;
        }

        if (other.tag == "Skill2_3")
        {
            OrcHP -= 500;

        }
        if (other.tag == "Skill2_4")
        {
            OrcHP -= 500;

        }
        if (other.tag == "Skill3_1")
        {
            OrcHP -= 500;

        }
        if (other.tag == "Skill3_2")
        {
            OrcHP -= 500;
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
                yield return new WaitForSeconds(0.1f);
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
