using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player_Scr : MonoBehaviour
{
    private void Awake() //해상도 설정
    {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Screen.SetResolution(1920, 1080, true);
    }

    public static Animator anim; 
	[Header("Player Health")]
	public float maxHP;
	public float HP = 100;
	public float MP = 100; //구르기 게이지 ,구르면 20씩 줄어들게 만들어둠

	public static float MainCharHP = 100f;

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

	private Transform groundCheck;
	public bool grounded = false;
	
	Rigidbody rigdbody;
	bool isJumping = false;
	public float jumpPower = 3.5f;
	public float rollPower = 3.5f;


	[Header("The fight")]
	public int damage;

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;

	/*-----AKH 수정-----*/
	//public GameObject crosshair;
	GameObject crosshair;
	/*-----AKH 수정-----*/

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

	bool roll_check = false;
	

	void CmdClientState(Vector3 targetPosVec,  float newRunWeight, float run, float strafe)
	{
		this.targetPosVec = targetPosVec;
		this.newRunWeight = newRunWeight;
		this.run = run;
		this.strafe = strafe;
	}

	void Start()
	{
		/*-----AKH 수정-----*/
		crosshair = GameObject.Find("Crosshair");
		/*-----AKH 수정-----*/

		dead = false;
		anim = GetComponent<Animator>();
		pa = GetComponent<Player_Autotarget> ();
		weapon1.SetActive(true);
		weapon2.SetActive(false);
		crosshair.SetActive(false);

		rigdbody = GetComponent<Rigidbody>();
		groundCheck = transform.Find("GroundCheck");

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

	void Update() 
	{
       // Debug.Log(MainCharHP);
		Locomotion ();
		Fight ();
		Health ();
		UI ();
		Jump();
		roll();
		//skill1();
		//땅체크
		grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Blocking")); 

	}
	/*void skill1()
    {



		if (Input.GetKeyDown("q") && isfight == true)
		{
			isshoot = true;
			anim.SetBool("isShoot", true);
		}
		else if (Input.GetKeyUp("q") && isfight == true)
		{
			isshoot = false;
			anim.SetBool("isShoot", false);
		}
	}*/
	void Jump()
    {
		
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("스페이스바");
			if (grounded == true)
			{
				Debug.Log("aaaaa");
				isJumping = true;
				anim.SetBool("Jump", Input.GetKey(KeyCode.Space));
			}

		}
	}

	void FixedUpdate() // 리지드바디 이용할 경우 update 대신 FixedUpdate 사용
	{
		if (isJumping == true)
		{
			Debug.Log("bbbb");
			rigdbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
			anim.SetBool("Jump", true);
			isJumping = false;

		}
		if (isJumping == false) //점프 한번만 가능하게 공중에서 false줌
		{
			anim.SetBool("Jump", false);
		}



	}

		void Locomotion()
	{
		targetPosVec = targetPos.position;


		run = Input.GetAxis("Vertical");
		strafe = Input.GetAxis("Horizontal");



		anim.SetFloat("Strafe", strafe); 
		anim.SetFloat("Run", run);

		if (roll_check == false)
		{

			//float forwardrun = 5 * Time.deltaTime;
			if (run > 0)
			{
				transform.Translate(Vector3.forward * (5 * Time.deltaTime) * run);
			}
			else if (run < 0)
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




			if (run != 0 || isfight == true)
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
	}


	void roll()
    {

		//roll 제어 코드
		if (Input.GetKeyDown(KeyCode.LeftShift) && isfight == false && run >= 0 && grounded == true)
		{
			MP -= 20;
			roll_check = true;
			anim.SetFloat("forward_roll", 1.0f);
			weapon1.SetActive(false);
			StartCoroutine("forward_roll_colltime");
			
		}

        else if(Input.GetKeyDown(KeyCode.LeftShift) && isfight == false && run < 0 && grounded == true)
		{
			MP -= 20;
			roll_check = true;
			Debug.Log("시발");
			anim.SetFloat("back_roll", 1.0f);
			weapon1.SetActive(false);
			
			StartCoroutine("back_roll_colltime");
		}

	}
	IEnumerator forward_roll_colltime()
    {
		
		yield return new WaitForSeconds(0.8f);
		weapon1.SetActive(true);	
		anim.SetFloat("forward_roll", 0.0f);
		roll_check = false;
	}
	IEnumerator back_roll_colltime()
    {
		yield return new WaitForSeconds(0.6f);
		weapon1.SetActive(true);	
		anim.SetFloat("back_roll", 0.0f);
		roll_check = false;
	}

	void Fight()
	{
		//AIMING
		if (Input.GetMouseButton(1) && isfight == false && roll_check == false) 
		{
			ShootSimple_Scr.SkillMode = true; //에임 조준 시 스킬 사용 가능
			isfight = true;
			anim.SetBool ("isFight", true);
			weapon2.SetActive(true);
			//weapon1.SetActive(false);
			crosshair.SetActive(true);
			Debug.Log("조준 활성화");
		} 
		else if (Input.GetMouseButtonUp(1) && isfight == true && roll_check == false) 
		{
			Debug.Log("조준 끝");
			weapon3.SetActive(false);
			//ShootSimple_Scr.SkillMode = false;
			isfight = false;
			anim.SetBool ("isFight", false);
			//weapon1.SetActive(true);
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
		sliderHP.value = MainCharHP;
		//healthText.text = "Health: " + MainCharHP.ToString ("0");

		if (MainCharHP <= 0)
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

    public void PlayerD(float damage)
    {
        //데미지만큼 체력 감소
        MainCharHP -= damage; // health = health - damage;

        //체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (MainCharHP <= 0 && !dead)
        {
            Death();
        }
    }
} 
