using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
public class test_Shoot : MonoBehaviourPun
{

    [Header("Weapon position")]
    public Transform weaponPoint;

    /*-----AKH 수정-----*/
    //public Transform aimPoint;
    GameObject CanvasAim;
    Transform aimPoint;
    /*-----AKH 수정-----*/

    [Header("Bullet start position")]
    public Transform shootPoint;
    [Header("The target followed by the camera")]
    public Transform targetLook;
    [Header("무기에 따른 스킬 제어")]
    public static int WeaponNumber = 3;


    [Header("Bullet")]
    public GameObject bullet;
    public GameObject skill1_1bullet;
    public GameObject skill1_2bullet;
    public GameObject skill1_4bullet;
    public GameObject skill1_4effect;

    public GameObject skill2_1bullet;
    public GameObject skill2_2bullet;

    public GameObject skill3_1bullet;


    [Header("If true, then you can shoot")]
    public bool isshoot = false;  //기본 공격 제어
    public bool isSkill1 = false;  //스킬1 제어
    public bool isSkill4 = false;  //스킬1 제어

    public bool isSkill2_1 = false;  //스킬1 제어
    public bool isSkill2_2 = false;  //스킬1 제어

    public bool isSkill3_1 = false;

    public static bool SkillMode = false;

    public GameObject weapon; //

    [Header("Bullet flight distance")]
    public float distance;
    [Header("Force")]
    public float force;
    [Header("Time between shots (Adjusts to the time of the shot animation)")]
    public float reloadTime;

    [Header("Time to remove the bullet")]
    public float shootFireLifeTime = 1;

    [Header("Shot sound")]
    public AudioClip fireSound;

    public ParticleSystem muzzleFlash;
    public ParticleSystem Shell;

    Vector3 startPos;
    Vector3 startRot;

    public float returnTime;

    AudioSource audioSource;

    void Start()
    {
        if (photonView.IsMine)
        {
            /*-----AKH 수정-----*/
            CanvasAim = GameObject.FindGameObjectWithTag("CanvasAim");
            //CanvasAim = GameObject.Find("CanvasAim");
            aimPoint = CanvasAim.GetComponent<Transform>();

            targetLook = GameObject.Find("TargetLook").GetComponent<Transform>();
            /*-----AKH 수정-----*/

            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            weaponPoint.LookAt(targetLook);
            aimPoint.LookAt(targetLook);
            Attack();
            if (WeaponNumber == 1)
            {
                skill1_1();
                skill1_4();
            }
            if (WeaponNumber == 2)
            {

                skill2_1();
                skill2_2();
            }

            if (WeaponNumber == 3)
            {

                skill3_1();
                //skill2_2();
            }
        }
    }

    void Attack()
    {
        if ((Input.GetMouseButton(0)) && (isshoot == true))
        {
            isshoot = false;
            //audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();
            Invoke("ShootTrue", reloadTime);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            GameObject myBullet = Instantiate(bullet);
            myBullet.transform.position = shootPoint.position;
            myBullet.transform.rotation = shootPoint.rotation;
            Destroy(myBullet, shootFireLifeTime);

        }
    }

    void skill1_1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkill1 == true)
        {
            Debug.Log("DASD");
            isSkill1 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();
            isshoot = false;
            Invoke("ShootTrue", reloadTime);
            Invoke("Skill1_True", 10.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            GameObject sk_bullet = Instantiate(skill1_1bullet);
            sk_bullet.transform.position = shootPoint.position;
            sk_bullet.transform.rotation = shootPoint.rotation;
            Destroy(sk_bullet, shootFireLifeTime);

            Player_Scr.anim.SetBool("isShoot", true);
            isSkill1 = true;
        }

        else if (Input.GetKeyUp(KeyCode.Alpha1) && isSkill1 == true)
        {
            isSkill1 = false;
            Player_Scr.anim.SetBool("isShoot", false);
        }
    }

    void skill1_4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && isSkill4 == true)
        {

            Debug.Log("4");
            isSkill4 = false;
            muzzleFlash.Play();
            Shell.Play();
            isshoot = false;
            Invoke("ShootTrue", reloadTime);
            Invoke("Skill4_True", 3);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }


            Player_Scr.anim.SetBool("isShoot", true);
            skill1_4effect.SetActive(true);
            StartCoroutine("Skill1_4");


        }
    }


    IEnumerator Skill1_4()
    {

        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(fireSound);
        GameObject sk_bullet = Instantiate(skill1_4bullet);
        sk_bullet.transform.position = shootPoint.position;
        sk_bullet.transform.rotation = shootPoint.rotation;
        Destroy(sk_bullet, shootFireLifeTime);
        Player_Scr.anim.SetBool("isShoot", false);
        skill1_4effect.SetActive(false);

    }


    void skill2_1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkill2_1 == true)
        {
            Debug.Log("DASD");
            isSkill2_1 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();
            isshoot = false;
            Invoke("ShootTrue", reloadTime);
            Invoke("Skill2_1_True", 3.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            GameObject sk_bullet = Instantiate(skill2_1bullet);
            sk_bullet.transform.position = shootPoint.position;
            sk_bullet.transform.rotation = shootPoint.rotation;
            Destroy(sk_bullet, shootFireLifeTime);
            isSkill2_1 = true;
            Player_Scr.anim.SetBool("isShoot", true);

        }

        else if (Input.GetKeyUp(KeyCode.Alpha1) && isSkill2_1 == true)
        {
            isSkill2_1 = false;
            Player_Scr.anim.SetBool("isShoot", false);
        }
    }
    void skill2_2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill2_2 == true && SkillMode == true)
        {
            Debug.Log("화염발사!");
            isSkill2_2 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();

            isshoot = false;
            Invoke("ShootTrue", 5.0f);
            Invoke("Skill2_2_True", 10.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            Player_Scr.anim.SetBool("isShoot", true);
            skill2_2bullet.SetActive(true);
            // isSkill2 = true;
            StartCoroutine("Skill2");
        }

        if (SkillMode == false)  //총 내리면 스킬 멈추기
        {
            Debug.Log("화염취소");
            skill2_2bullet.SetActive(false);
        }


    }

    IEnumerator Skill2()
    {

        yield return new WaitForSeconds(5.0f);
        Player_Scr.anim.SetBool("isShoot", false);
        skill2_2bullet.SetActive(false);
    }

    void skill3_1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkill3_1 == true)
        {
            Debug.Log("DASD");
            isSkill3_1 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();
            isshoot = false;
            Invoke("ShootTrue", reloadTime);
            Invoke("Skill3_1_True", 5.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            GameObject sk_bullet = Instantiate(skill3_1bullet);
            sk_bullet.transform.position = shootPoint.position;
            sk_bullet.transform.rotation = shootPoint.rotation;
            Destroy(sk_bullet, shootFireLifeTime);

            Player_Scr.anim.SetBool("isShoot", true);
            isSkill3_1 = true;
        }

        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            isSkill3_1 = false;
            Player_Scr.anim.SetBool("isShoot", false);
        }
    }




    void ShootTrue()
    {
        isshoot = true;
    }

    void Skill1_True()
    {
        isSkill1 = true;
    }
    void Skill4_True()
    {
        isSkill4 = true;
    }
    void Skill2_1_True()
    {
        Debug.Log("인보크실행");
        isSkill2_1 = true;
    }
    void Skill2_2_True()
    {
        isSkill2_2 = true;
    }

    void Skill3_1_True()
    {
        isSkill3_1 = true;
    }

}
