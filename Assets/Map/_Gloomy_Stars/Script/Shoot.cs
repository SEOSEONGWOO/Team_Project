using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject skill1_bullet;
    public GameObject skill2_bullet;
    public float power = 1500f;

    public static float attackCoolTime = 1.0f; //일반 공격 쿨타임
    public static float timer =0;

    public static float skill1_CoolTime; //스킬1 공격 쿨타임
    public static float skill2_CoolTime;

    public static float skill1_timer; //스킬2 공격 쿨타임
    public static float skill2_timer;

    public static float Dalaytimer;

    // Start is called before the first frame update
    void Start()
    {
        
        skill1_CoolTime = 5.0f;
        skill2_CoolTime = 5.0f;

        skill1_timer = 0;
        skill2_timer = 0;
        Dalaytimer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //기본공격 1초마다 나가기
        
        skill1_timer += Time.deltaTime; //스킬1 5초마다 나가기
        skill2_timer += Time.deltaTime; //스킬2 5초마다 나가기
        
        if (timer >= attackCoolTime && PlayerController.grounded == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerController.isAttack = true;
                GameObject ins = Instantiate(bullet, transform.position +new Vector3(0,0,3), transform.rotation) as GameObject;
                ins.GetComponent<Rigidbody>().AddForce(transform.forward * power);
                timer = 0;
            }
        }

        if (skill1_timer >= skill1_CoolTime && PlayerController.grounded == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                PlayerController.isSkill1 = true;
                StartCoroutine(Skill1());
                skill1_timer = 0;
            }
        }

        if (skill2_timer >= skill2_CoolTime && PlayerController.grounded == true)
        {
            if (Input.GetMouseButtonDown(2))
            {
                PlayerController.isSkill2 = true;
                StartCoroutine(Skill2());
                skill2_timer = 0;
            }
        }
        IEnumerator Skill1()
        {
            yield return new WaitForSeconds(0.7f);
            GameObject ins1 = Instantiate(skill1_bullet, transform.position, transform.rotation) as GameObject;
            ins1.GetComponent<Rigidbody>().AddForce(transform.forward * power);
            
        }
        IEnumerator Skill2()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject ins2 = Instantiate(skill2_bullet, transform.position, transform.rotation) as GameObject;
            ins2.GetComponent<Rigidbody>().AddForce(transform.forward * power);
            

        }

        /*void Skill2_shoot()
        {
            
                GameObject ins2 = Instantiate(skill2_bullet, transform.position, transform.rotation) as GameObject;
                ins2.GetComponent<Rigidbody>().AddForce(transform.forward * power);
                skill2_timer = 0;
            
        }*/






    }
}
