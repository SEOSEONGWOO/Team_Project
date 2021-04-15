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
	[Header("Bullet")]
    public GameObject bullet;
    public GameObject skill1_bullet;

    [Header("If true, then you can shoot")]
    public bool isshoot = false;  //기본 공격 제어
    public bool isSkill1 = false;  //스킬1 제어


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
        skill1();
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


    void skill1()
    {
        if (Input.GetKeyDown("q") && isSkill1 == true )
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
            GameObject sk_bullet = Instantiate(skill1_bullet);
            sk_bullet.transform.position = shootPoint.position;
            sk_bullet.transform.rotation = shootPoint.rotation;
            Destroy(sk_bullet, shootFireLifeTime);

            Player_Scr.anim.SetBool("isShoot", true);
            isSkill1 = true;
        }

        else if (Input.GetKeyUp("q") && isSkill1 == true)
        {
            isSkill1 = false;
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
}
