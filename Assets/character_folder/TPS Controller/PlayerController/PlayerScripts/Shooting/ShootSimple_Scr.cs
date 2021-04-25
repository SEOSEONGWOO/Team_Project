using UnityEngine;
using System.Collections;

public class ShootSimple_Scr : MonoBehaviour 
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
    public static int WeaponNumber=1;


    [Header("Bullet")]
    public GameObject bullet;
    public GameObject skill1_1bullet;
    public GameObject skill1_2bullet;
    public GameObject skill2_1_bullet;
    public GameObject skill2_2_bullet;
    public GameObject skill3_1_bullet;


    [Header("If true, then you can shoot")]
    public bool isshoot = false;  //기본 공격 제어
    public bool isSkill1 = false;  //스킬1 제어
    public bool isSkill2 = false;  //스킬1 제어
    
    public static bool SkillMode = false;

    public GameObject weapon; //

    [Header("Bullet flight distance")]
    public float distance;
	[Header("Force")]
    public float force;
	[Header("Time between shots (Adjusts to the time of the shot animation)")]
    public float reloadTime;

	[Header("Time to remove the bullet")]
    public float shootFireLifeTime;

	[Header("Shot sound")]
	public AudioClip fireSound;

    public ParticleSystem muzzleFlash;
    public ParticleSystem Shell;

    Vector3 startPos;
    Vector3 startRot;

    public float returnTime;

    AudioSource audioSource;

    void Start () 
    {

        /*-----AKH 수정-----*/
        CanvasAim = GameObject.Find("CanvasAim");
        aimPoint = CanvasAim.GetComponent<Transform>();
        /*-----AKH 수정-----*/

        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () 
    {
        weaponPoint.LookAt(targetLook);
        aimPoint.LookAt(targetLook);
        Attack();
        if(WeaponNumber == 1)
        {
            skill1_1();
            
            
        }

        if (WeaponNumber == 2)
        {
            
            skill2_1fire();
        }


    }

     void Attack()
        {
            if ((Input.GetMouseButton(0)) && (isshoot == true))
            {
                isshoot = false;
                audioSource.PlayOneShot(fireSound);
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
    void skill2_2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill1 == true)
        {
            Debug.Log("DASD");
            isSkill1 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();
            isshoot = false;
            Invoke("ShootTrue", reloadTime);
            Invoke("Skill1_True", 5.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }
            GameObject sk_bullet = Instantiate(skill1_2bullet);
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
    void skill2_1fire()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill2 == true)
        {
            Debug.Log("스킬25 실행");
            isSkill2 = false;
            audioSource.PlayOneShot(fireSound);
            muzzleFlash.Play();
            Shell.Play();

            isshoot = false;
            Invoke("ShootTrue", 5.0f);
            Invoke("Skill2_True", 10.0f);
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, distance))
            {
                if (hit.transform.GetComponent<Rigidbody>())
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(shootPoint.forward * force, hit.point);
                }
            }     
            Player_Scr.anim.SetBool("isShoot", true);
            skill2_2_bullet.SetActive(true);
           // isSkill2 = true;
            StartCoroutine("Skill2");
        }

        if (SkillMode == false)  //총 내리면 스킬 멈추기
        {
            skill2_2_bullet.SetActive(false);
        }
        

    }

    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(5.0f);
        Player_Scr.anim.SetBool("isShoot", false);
        skill2_2_bullet.SetActive(false);
    }

   

   


    void ShootTrue() 
    {
        isshoot = true;
    }

    void Skill1_True()
    {
        isSkill1 = true;
    }

    void Skill2_True()
    {
        isSkill2 = true;
    }
}
