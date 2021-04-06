using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player_Scr : MonoBehaviour
{
	Animator anim; 
	[Header("Player Health")]
	public float maxHP;
	public float HP = 100;

	public float lookIKWeight;
	public float bodyWeight;


	[Tooltip("Health text")]
	public Text healthText;
	public Slider sliderHP;

    [Header("Character customization")]
	public Transform targetPos; 
	public Transform targetPosOld;
	public float angularSpeed; 
	bool isPlayerRot; 
	public float luft;

	[Header("The fight")]
	public int damage;

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject crosshair;

	[Header("Battle mode")]
	[HideInInspector]
	public bool isfight = false;
	public bool isshoot = false;
	public GameObject fightModUI;

	[Header("Ragdoll Player")]
	public GameObject ragdoll;
	public static bool dead = false;

	Vector3 targetPosVec;
	float newRunWeight = 1f;
	float run = 0f;
	float strafe = 0f;

	Player_Autotarget pa;

	void CmdClientState(Vector3 targetPosVec,  float newRunWeight, float run, float strafe)
	{
		this.targetPosVec = targetPosVec;
		this.newRunWeight = newRunWeight;
		this.run = run;
		this.strafe = strafe;
	}

	void Start()
	{ 
		dead = false;
		anim = GetComponent<Animator>();
		pa = GetComponent<Player_Autotarget> ();
		weapon1.SetActive(true);
		weapon2.SetActive(false);
		crosshair.SetActive(false);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

	void Update() 
	{
		Locomotion ();
		Fight ();
		Health ();
		UI ();

		
		

	}

	void Locomotion()
	{
		targetPosVec = targetPos.position;


		run = Input.GetAxis("Vertical");
		strafe = Input.GetAxis("Horizontal"); 

		anim.SetFloat("Strafe", strafe); 
		anim.SetFloat("Run", run);

		//float forwardrun = 5 * Time.deltaTime;
		if (run > 0)
        {
			transform.Translate(Vector3.forward * (5 * Time.deltaTime) * run);
        }
		else if(run < 0)
        {
			{
				transform.Translate(Vector3.forward * (3 * Time.deltaTime) * run);
			}
		}

		if (strafe > 0)
		{
			transform.Translate(Vector3.right * (3 * Time.deltaTime) * strafe);
		}
		else if (strafe < 0)
		{
			{
				transform.Translate(Vector3.right * (3 * Time.deltaTime) * strafe);
			}
		}




		if (run !=0 || isfight == true)
			{
				Vector3 rot = transform.eulerAngles; 
				transform.LookAt(targetPosVec);
				float angleBetween = Mathf.DeltaAngle(transform.eulerAngles.y, rot.y); 
				if ((Mathf.Abs(angleBetween) > luft) || strafe != 0) 
				{
					isPlayerRot = true; 
				}
				if (isPlayerRot == true) 
				{
					float bodyY = Mathf.LerpAngle(rot.y, transform.eulerAngles.y, Time.deltaTime * angularSpeed);
					transform.eulerAngles = new Vector3(0, bodyY, 0); 

					if (strafe == 0) 
					{
						anim.SetBool("Turn", false); 
					}
					else
					{
						anim.SetBool("Turn", false); 
					}

					if (Mathf.Abs(angleBetween) * Mathf.Deg2Rad <= Time.deltaTime * angularSpeed) 
					{
						isPlayerRot = false; 
						anim.SetBool("Turn", false); 
					}
				}
				else
				{
					transform.eulerAngles = new Vector3(0f, rot.y, 0f);  
				}
			}
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
	}

	void Fight()
	{
		//AIMING
		if (Input.GetMouseButton(1) && isfight == false) 
		{
			isfight = true;
			anim.SetBool ("isFight", true);
			weapon2.SetActive(true);
			weapon1.SetActive(false);
			crosshair.SetActive(true);
		} 
		else if (Input.GetMouseButtonUp(1) && isfight == true) 
		{
			isfight = false;
			anim.SetBool ("isFight", false);
			weapon1.SetActive(true);
			weapon2.SetActive(false);
			crosshair.SetActive(false);
			isshoot = false;
			anim.SetBool("isShoot", false);
		}
		//SHOOTING
		if (Input.GetMouseButton(0) && isfight == true)
        {
			isshoot = true;
			anim.SetBool("isShoot", true);
		}
		else if (Input.GetMouseButtonUp(0) && isfight == true)
		{
			isshoot = false;
			anim.SetBool("isShoot", false);
		}
	}

	void OnAnimatorIK()
	{
		if (Input.GetMouseButton(1))
		{
			anim.SetLookAtWeight(lookIKWeight, bodyWeight);
		    anim.SetLookAtPosition(targetPosVec);
		}
	}

	void Health()
	{
		sliderHP.maxValue = maxHP;
		sliderHP.value = HP;
		healthText.text = "Health: " + HP.ToString ("0");

		if (HP <= 0)
			Death();

        //-----------HP recovery--------------//

        if (HP < maxHP) 
		{
			HP += Time.deltaTime * 0.5f;
		}
		//------------------------------------------//
	}

	void UI()
	{
		if (isfight)
			fightModUI.SetActive (true);
		else
			fightModUI.SetActive (false);
	}


	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Damage")
		{
			{
				HP -= Time.deltaTime * 20f;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "DamageAI")
		{
			{
				HP -= 50;
			}
		}
	}

	public void Death()
	{
		Destroy(gameObject);
		Instantiate (ragdoll, transform.position, transform.rotation);
		dead = true;
	}
} 
