using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player_Scr : MonoBehaviourPun
{

    // 죽었을 때 스폰지역으로 다시 돌아가는 거 만드셈ㅇ ㅋ? 『순간이동』


	private void Awake() //해상도 설정
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Screen.SetResolution(1920, 1080, true);
	}

	public static Animator anim;
	[Header("Player Health")]
	public static float maxHP = 100;
	public static float HP = 100;
    public static float maxMP = 100;
    public static float MP = 100; //구르기 게이지 ,구르면 20씩 줄어들게 만들어둠

	public int Depense = 5; // 방어력

	public int HitD = 0; // 맞는 데미지

	public static float MainCharHP = 100f;

	public float lookIKWeight;
	public float bodyWeight;
	//public GameObject a;

	[Header("clear_time")]
	public static string player_name;
	public static float clear_time =1.2f;

	[Tooltip("Health text")]
	public Text healthText;
	public Slider sliderHP;
	[Header("Money")]
	public static int money = 5000;

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

	[Header("무기스킬1 제어")]
	public bool isSkill2 = true;
	public bool isSkill3 = true;
	public GameObject skill2_Effect;
	public GameObject skill3_Buff_Effect;

	[Header("무기스킬2 제어")]
	public bool isSkill2_3 = true;
	public bool isSkill2_4 = true;
	public GameObject skill2_fireFiled;
	public GameObject skill2_meteo;

	[Header("무기스킬2 제어")]
	public bool isSkill3_2 = true;
	public bool isSkill3_3 = true;
	public bool isSkill3_4 = true;
	public GameObject skill3_wall;
	public GameObject skill3_buff;
	public GameObject skill3_4_Effect;




	[Header("The fight")]
	public int damage;

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;

	/*-----AKH 수정-----*/
	GameObject crosshair;
	public Transform tr;

	public static bool isShop = true;

	public static bool isdead = false;

	/*-----AKH 수정-----*/

	[Header("Battle mode")]
	[HideInInspector]
	public bool isfight = false;
	public bool isshoot = false;
	public GameObject fightModUI;

	[Header("Ragdoll Player")]
	public GameObject ragdoll;
	public static bool dead = false;  //안씀 
	//public bool die = false;

	Vector3 targetPosVec;
	float newRunWeight = 1f;
	float run = 0f;
	float strafe = 0f;

	Player_Autotarget pa;

	bool roll_check = false;


	// 플레이어 따라오게 하는 코드
	//public Transform clcl;
	public static Vector3 CLC;

    // public Transform FirstLocation;

    public Vector3 FirstLocationVector;


    public GameObject Burned;
    public static bool FireM = false;
    public static float FireTime = 0f;
    float FireDamage = 0.05f;


	void CmdClientState(Vector3 targetPosVec, float newRunWeight, float run, float strafe)
	{
		this.targetPosVec = targetPosVec;
		this.newRunWeight = newRunWeight;
		this.run = run;
		this.strafe = strafe;
	}

	void Start()
	{
        FirstLocationVector = gameObject.transform.position;
		/*-----AKH 수정-----*/
		DontDestroyOnLoad(gameObject);

		targetPos = GameObject.Find("TargetLook").GetComponent<Transform>();
		targetPosOld = GameObject.Find("TargetLookInFight").GetComponent<Transform>();

		crosshair = GameObject.FindGameObjectWithTag("Crosshair");

		tr = GetComponent<Transform>();
		
		/*-----AKH 수정-----*/

		dead = false;
		anim = GetComponent<Animator>();
		pa = GetComponent<Player_Autotarget>();
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
		/*if (!photonView.IsMine)
		{
			return;
		}*/
		if (isShop)
		{
			if (dead == false) 
			{
				if (HP <= 0)
				{
					Dead();
				}
			}

			CLC = gameObject.transform.position;

            if (FireM == true)
			{
				isFireM();

            }

			Locomotion();
			Fight();
			Jump();
			roll();
			w_change();

			clear_time += Time.deltaTime; //시작 시 시간 측정
			
			if (ShootSimple_Scr.WeaponNumber == 1)
			{
				skill1_2();
				skill1_3SpeedBuff();
			}

			else if (ShootSimple_Scr.WeaponNumber == 2)
			{
				skill2_3();
				skill2_4();
			}

			else if (ShootSimple_Scr.WeaponNumber == 3)
			{
				skill3_2();
				skill3_3();
				skill3_4();
			}
			

			ManaRegen();//마나 초당 회복
			MaxHp();// 체력 100이상 되면 100으로 고정
			grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Blocking"));
		}

		
	}

	
	void ManaRegen()
	{
		if (MP != maxMP)
		{
			//마나는 초당 0.5씩 회복
			MP += Time.deltaTime * 10f;

			//마나를 회복했는데 최대 마나보다 크다면 현재마나를 최대마나량으로 변경
			if (MP > maxMP)
			{
				MP = maxMP;
			}
			//회복된 마나량을 Mpbar슬라이더에 업데이트
		}
	}

	void MaxHp()
	{

		if (HP > maxHP)
		{
			HP = maxHP;
		}

	}

	void w_change() //스킬 체인지
	{
		if (SkillPanelMove.skill_mode == 1) //스킬 ui가 불속성으로 바뀌면 불속성 스킬 사용가능
		{
			ShootSimple_Scr.WeaponNumber = 2;

		}
		if (SkillPanelMove.skill_mode == 2) //스킬 ui가 전기속성으로 바뀌면 속성 스킬 사용가능
		{
			ShootSimple_Scr.WeaponNumber = 1;

		}
		if (SkillPanelMove.skill_mode == 3) //스킬 ui가 빛속성으로 바뀌면 빛속성 스킬 사용가능
		{
			ShootSimple_Scr.WeaponNumber = 3;

		}
	}


	public void skill1_2()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill2 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false) //에임 조준 안했을시 2번 누르면 실행

		{
			anim.SetBool("Skill_1_Magic", true);
			isSkill2 = false;
			isfight = true;
			StartCoroutine("Skill1_2");


		}
	}

	IEnumerator Skill1_2()
	{

		yield return new WaitForSeconds(2.0f);
		anim.SetBool("Skill_1_Magic", false);
		Instantiate(skill2_Effect, transform.position + this.transform.forward * 5, Quaternion.Euler(-90, 0, 0));
		isfight = false;
		isSkill2 = true;

	}
	public void skill1_3SpeedBuff()
	{
		if (Input.GetKeyDown(KeyCode.Alpha3) && isSkill3 == true)
		{

			isSkill3 = false;
			skill3_Buff_Effect.SetActive(true); //버프 이펙트 나오게하기
			Bullet.bulletDamage += 20; //캐릭터 방어력 5 상승
			StartCoroutine("Skill1_3");
		}
	}


	IEnumerator Skill1_3()
	{

		yield return new WaitForSeconds(5.0f);
		Bullet.bulletDamage -= 20;
		 //캐릭터 방어력 원상태로 돌리기
		skill3_Buff_Effect.SetActive(false);
		isSkill3 = true;

	}
	
	public void skill2_3()
	{
		if (Input.GetKeyDown(KeyCode.Alpha3) && isSkill2_3 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false)
		{

			isSkill2_3 = false;
			skill2_fireFiled.SetActive(true); //버프 이펙트 나오게하기			
			StartCoroutine("Skill2_3");
		}
	}

	IEnumerator Skill2_3()
	{
		yield return new WaitForSeconds(5.0f); //초후 버프 없애기
		skill2_fireFiled.SetActive(false);
		yield return new WaitForSeconds(8.0f); //스킬 쿨타임
		isSkill2_3 = true;
	}
	public void skill2_4()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4) && isSkill2_4 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false) //에임 조준 안했을시 2번 누르면 실행

		{
			anim.SetBool("Skill_1_Magic", true);
			isSkill2_4 = false;
			isfight = true;
			StartCoroutine("Skill2_4");


		}
	}

	IEnumerator Skill2_4()
	{

		yield return new WaitForSeconds(2.0f);
		anim.SetBool("Skill_1_Magic", false);
		Instantiate(skill2_meteo, transform.position + this.transform.forward * 10, Quaternion.Euler(-90, 0, 0));
		isfight = false;
		yield return new WaitForSeconds(10.0f);
		isSkill2_4 = true;

	}

	public void skill3_2()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill3_2 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false) //에임 조준 안했을시 2번 누르면 실행

		{	
			isSkill3_2 = false;
			isfight = true;
			StartCoroutine("Skill3_2");
			Instantiate(skill3_wall, transform.position + this.transform.forward * 2 + this.transform.up * 1, Quaternion.Euler(-90, 0, 0));
			isfight = false;
		}
	}

	IEnumerator Skill3_2()
	{

		yield return new WaitForSeconds(5.0f); //스킬 쿨
		//Destroy(skill3_wall);
		isSkill3_2 = true;

	}

	public void skill3_3()
	{
		if (Input.GetKeyDown(KeyCode.Alpha3) && isSkill3_3 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false)
		{

			isSkill3_3 = false;
			skill3_buff.SetActive(true); //버프 이펙트 나오게하기	
			Depense += 50;
			StartCoroutine("Skill3_3");
		}
	}
	IEnumerator Skill3_3()
	{

		yield return new WaitForSeconds(5.0f); //초후 버프 없애기
		skill3_buff.SetActive(false);
		Depense -= 50;
		yield return new WaitForSeconds(8.0f); //스킬 쿨타임
		isSkill3_3 = true;
	}
	public void skill3_4()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4) && isSkill3_4 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false)
		{
			isfight = true;
			isSkill3_4 = false;
			anim.SetBool("Skill_1_Magic", true);
			 //버프 이펙트 나오게하기			
			StartCoroutine("Skill3_4");
		}
	}

	IEnumerator Skill3_4()
	{
		yield return new WaitForSeconds(2.0f);
		skill3_4_Effect.SetActive(true);
		anim.SetBool("Skill_1_Magic", false);
		isfight = false;
		yield return new WaitForSeconds(5.0f); //초후 버프 없애기
		skill3_4_Effect.SetActive(false);
		hill_buff.a = false; //초당 체력 재생 변수 끄기
		yield return new WaitForSeconds(8.0f); //스킬 쿨타임
		isSkill3_4 = true;

	}

	





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
			ShootSimple_Scr.SkillMode = false;
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
		if (Input.GetMouseButton(1) && isShop)
		{
			anim.SetLookAtWeight(lookIKWeight, bodyWeight);
		    anim.SetLookAtPosition(targetPosVec);
			//a.transform.rotation = Quaternion.Euler(targetPosVec);
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

	/*void UI()
	{
		if (isfight)
			fightModUI.SetActive (true);
		else
			fightModUI.SetActive (false);
	}*/

    void isFireM()
    {
        Debug.Log(HP);
        Debug.Log(FireTime);
        if(FireTime < 4.0f)
        {
            FireTime += Time.deltaTime;
            HP -= FireDamage;
            Burned.SetActive(true);
        }
        else if(FireTime >= 4.0f)
        {
            FireM = false;
            Burned.SetActive(false);
        }
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
	public void Dead()
    {
		anim.SetTrigger("Die2"); //죽는 애니메이션 실행
		dead = true;
		roll_check = true;  //구르기 상태로 만들어서 움직이는 기능 멈추기
		StartCoroutine("die");
		//Playerspawn.count = true;   //죽을때 텔포 한번만
	}
	IEnumerator die()
	{
		yield return new WaitForSeconds(1.5f); //캐릭터 삭제시 ~초후 실행
		gameObject.transform.position = FirstLocationVector;
		HP = 100;
		anim.SetTrigger("Die1");
		isdead = true;	//죽을때 텔포
		dead = false;
		roll_check = false;
	}

	public void Death() //사망시 실행
	{
		//anim.SetBool("Die", true);  //사망 애니메이션
		Destroy(gameObject);  //3초후 오브젝트 삭제
		Instantiate(ragdoll, transform.position, transform.rotation);
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

    public void GunnerHitFunc(int damage)   // 
    {
        HitD = damage - Depense;
        if (HitD >= 1)
        {
            HP = HP - (float)HitD;
            Debug.Log(HitD);
        }
        else if (HitD < 1)
        {
            HP = HP - 1f;
            Debug.Log(HitD);
        }
    }

	


} 
