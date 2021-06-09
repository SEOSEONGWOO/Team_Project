using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KissZombie : MonoBehaviour
{
    public Animator avatar;

    public Transform firstlo;   // 처음 좀비 위치
    private NavMeshAgent nav;

    //public GameObject SpCoin; // 죽으면 떨어뜨릴 코인

    //public GameObject ZBC;

    public GameObject Gobj; // 따라갈 캐릭터 게임오브젝트 넣어줄 빈 값

    public Vector3 fl1, fl2, fl3;

    private float attackRange = 2.3f;   // 미구현 구현예정 ( 아직안쓰임 )

    public int test555; // 시작할 때 값 바로 돌아가기로 넣으면 왠지 모르는데 y 값이 0.16 올라가서 시작되서 그거 막아주려고 시작하고 위치값 바뀌면 그 로케이션 저장하도록 막아주는 장치
    int Chp = 100; // 체력
    int ZomA = 10; // 데미지

    float delay = 0f; // 죽고 몇초뒤에 사라질 지 
    float DMdelay = 0f; // 공격 할 때 딜레이

    public Transform ZombieLo;   // 스켈레톤 위치

    public Vector3 ZombieVec; // 스켈레톤 위치 vector값

    float distance; // 스켈레톤 , 플레이어 거리 넣어 줄 float

    public AudioClip audioV;
    public AudioClip audioHit;
    public AudioClip audioDie;

    AudioSource audioSource;



    private void Start()
    {
        avatar = GetComponent<Animator>();
        test555 = 1; // 이거 안 넣으면 0.16 y 값 올라가는거 못 잡음 왠지 모름 알면 수정좀

    }

    void Update()
    {
        this.audioSource = GetComponent<AudioSource>();

        Gobj = GameObject.FindGameObjectWithTag("Player");  // 시작할 때 태그 이름이 Player 인 거 Gobj 오브젝트에 넣어 줌

        fl3 = firstlo.transform.position;   // fl3 = 현재 좀비 위치 값
        Quaternion rot; // 회전값
        rot = Quaternion.Euler(Random.Range(0, 0), Random.Range(0, 0), Random.Range(0, 0)); // 랜덤 회전 값

        ZombieVec = ZombieLo.transform.position; // 스켈레톤 현재 위치값 

        distance = Vector3.Distance(ZombieVec, Player_Scr.CLC);

        /*if (Input.GetButtonDown("Fire1"))
        {
            Chp -= 10;
        }*/

        if (avatar.GetBool("Die") == true) // 좀비 죽은 상태가 되면
        {
            delay += Time.deltaTime;    // 딜레이 높이고


            if (delay >= 3) // 3초 지나면
            {
                Destroy(gameObject, 2.0f); // 좀비 사라짐
                delay = 0; // 딜레이 초기화
            }
        }

        if (Chp <= 0 && avatar.GetBool("Die") == false) // 체력 0 낮고 죽은 상태 아니면 
        {
            avatar.SetTrigger("CDie");  // 죽은상태 트리거 on
            avatar.SetBool("Die", true);    // 죽은 상태로 바꿔 줌
            StartCoroutine(PlaySound("Die"));
            //Instantiate(SpCoin, new Vector3(fl3.x, fl3.y - 1, fl3.z), rot); // 현재 좀비 위치에 돈 소환함

        }

        if (avatar.GetBool("Die") == false) // 좀비 안 죽었을 때
        {
            if (test555 == 1)   // 처음 체크
            {
                fl1 = firstlo.transform.position; // 시작 위치 값
                fl2 = firstlo.transform.eulerAngles; // 시작할 때 보고있는 각도 
                test555 = 0; // 계속 체크되지않게 초기롸
            }
            Quaternion WcL = Quaternion.Euler(fl2.x, fl2.y, fl2.z); // 처음 보고있는 각도 값 WcL 에 넣어줌 

            nav = GetComponent<NavMeshAgent>();


            if (avatar.GetBool("Att") == true) // 공격 중일 때
            {
                nav.speed = 0;  // 제자리 멈추게 설정
                if (DMdelay <= 1f)  // 1초마다
                {
                    DMdelay = DMdelay + Time.deltaTime;
                }
                else if (DMdelay > 1f)
                {
                    Gobj.GetComponent<Player_Scr>().GunnerHitFunc(ZomA); // 공격
                    StartCoroutine(PlaySound("Attack1"));
                    DMdelay = 0f;
                }
            }
            else if (avatar.GetBool("Att") == false) // 공격 중 아닐 때
            {
                DMdelay = 0f;
                if (distance < 10.0f) // 인식범위에 들어오면
                {
                    nav.speed = 4;  // 4의 속도로
                    nav.SetDestination(Player_Scr.CLC); // Gunner.CLC = 현재 캐릭터 위치 로 네비설정(쫓아가도록 설정)
                    avatar.SetBool("LookC", true); // LookCharacter .. 캐릭터 쪽으로 달람
                }
                if (distance > 20.0f) // 인식범위 밖이면
                {
                    avatar.SetBool("LookC", false); // LookCharacter 끄고
                    nav.SetDestination(fl1); // 처음 위치로 돌아감
                    if (firstlo.position == fl1) // 처음 위치랑 같으면
                    {
                        avatar.SetBool("BackP", false); //돌아가는 모션 끔
                        // firstlo.transform.rotation = Quaternion.Euler(fl2);
                        if (firstlo.transform.eulerAngles != new Vector3(fl2.x, fl2.y, fl2.z))// 원래 있던 자리 보고있던 방향 안보고있으면 
                        {
                            //firstlo.transform.eulerAngles += new Vector3(0f, 0.1f, 0f);
                            firstlo.transform.rotation = Quaternion.Lerp(firstlo.transform.rotation, WcL, Time.deltaTime * 1);      //원래 보던 곳으로 

                        }

                    }
                    else if (firstlo.position != fl1) // 처음 위치가 아니면
                    {
                        nav.speed = 1; // 속도 1로 제자리로 돌아감.
                        avatar.SetBool("BackP", true); // 돌아가는 모션
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") //Player 태그가진 캐릭터가 공격 범위 들어오면
        {
            avatar.SetBool("Att", true); // 공격 true
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") //Player 태그가진 캐릭터가 공격 범위 나가면 
        {
            avatar.SetBool("Att", false); // 공격 false
        }
    }

    public void SetDamageAI()
    {
        Chp -= Bullet.bulletDamage; // 총에 맞으면 총알데미지 만큼 체력 까임
        StartCoroutine(PlaySound("Hit"));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Skill1")
        {
            Chp -= 500;
        }

        if (other.tag == "skill1_2")
        {
            Chp -= 500;

        }
        if (other.tag == "Skill1_4")
        {
            Chp -= 500;
        }

        if (other.tag == "Skill2_1")
        {
            Chp -= 500;
        }

        if (other.tag == "Skill2_3")
        {
            Chp -= 500;

        }
        if (other.tag == "Skill2_4")
        {
            Chp -= 500;

        }
        if (other.tag == "Skill3_1")
        {
            Chp -= 500;

        }
        if (other.tag == "Skill3_2")
        {
            Chp -= 500;
        }
    }

    IEnumerator PlaySound(string action)
    {
        switch (action)
        {
            case "Attack1":
                yield return new WaitForSeconds(0.1f);
                audioSource.clip = audioV;
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
