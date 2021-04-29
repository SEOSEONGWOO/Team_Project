﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Utility;
using Photon.Pun;
using Photon.Realtime;


public class PlayerCs : MonoBehaviour
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

	public int Depense = 5; // 방어력

	public int HitD = 0; // 맞는 데미지

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

	/*[Header("무기스킬1 제어")]
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
	public GameObject skill3_wall;
	public GameObject skill3_buff;*/




	[Header("The fight")]
	public int damage;

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;

	/*-----AKH 수정-----*/
	//public GameObject crosshair;
	GameObject crosshair;

	//public static PhotonView pv;
	/*-----AKH 수정-----*/

	[Header("Battle mode")]
	[HideInInspector]
	public bool isfight = false;
	public bool isshoot = false;
	public GameObject fightModUI;

	/*[Header("Ragdoll Player")]
	public GameObject ragdoll;
	public static bool dead = false;*/

	Vector3 targetPosVec;
	float newRunWeight = 1f;
	float run = 0f;
	float strafe = 0f;

	Player_Autotarget pa;

	bool roll_check = false;


	/*// 플레이어 따라오게 하는 코드
	public Transform clcl;*/

	public static Vector3 CLC;

	// 돈 관련 코드

	/*public static int PlayerMoney;
	public GameObject PlayerMoneyText;*/

	public float speed = 5.0f;
	public float rotSpeed = 120.0f;
	public Transform tr;
	public PhotonView pv;

	private Quaternion currRot;
	private Vector3 currPos;


	void CmdClientState(Vector3 targetPosVec, float newRunWeight, float run, float strafe)
	{
		this.targetPosVec = targetPosVec;
		this.newRunWeight = newRunWeight;
		this.run = run;
		this.strafe = strafe;
	}

	void Start()
	{
		/*-----AKH 수정-----*/

		targetPos = GameObject.Find("TargetLook").GetComponent<Transform>();
		targetPosOld = GameObject.Find("TargetLookInFight").GetComponent<Transform>();

		crosshair = GameObject.Find("Crosshair");


		tr = GetComponent<Transform>();
		pv = GetComponent<PhotonView>();

		//동기화 콜백함수가 발생하려면 반드시 필요
		pv.ObservedComponents[0] = this;
		/*-----AKH 수정-----*/

		//dead = false;
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
		//CLC = clcl.transform.position;

		// 돈 텍스트

		//  string PlayerMoneyT = "보유 금액 : " + PlayerMoney;
		//  PlayerMoneyText.GetComponent<Text>().text = PlayerMoneyT;


		// Debug.Log(MainCharHP);

		if (!pv.IsMine)
		{
			//네트워크로 연결된 다른 유저일 경우에 실시간 전송 받는 변수를 이용해서 이동
			tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
			tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
		}

		Locomotion();
		Fight();
		Health();
		UI();
		Jump();
		roll();

		/*if (ShootSimple_Scr.WeaponNumber == 1)
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
		}*/
		w_change(); //무기변경 코드
					//skill1();
					//땅체크
		grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Blocking"));


	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			//자신의 플레이어 정보는 다른 네트워크 사용자에게 송신
			stream.SendNext(tr.position);
			stream.SendNext(tr.rotation);
		}
		else
		{
			//다른 플레이어의 정보는 수신
			currPos = (Vector3)stream.ReceiveNext();
			currRot = (Quaternion)stream.ReceiveNext();
		}
	}
	void w_change()
	{

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Debug.Log("무기변경");
			if (ShootSimple_Scr.WeaponNumber == 1)
			{
				Debug.Log("무기변경");
				ShootSimple_Scr.WeaponNumber = 2;
			}
			else if (ShootSimple_Scr.WeaponNumber == 2)
			{
				Debug.Log("무기변경");
				ShootSimple_Scr.WeaponNumber = 3;
			}
			else if (ShootSimple_Scr.WeaponNumber == 3)
			{
				Debug.Log("무기변경");
				ShootSimple_Scr.WeaponNumber = 1;
			}
		}
	}


	/*public void skill1_2()
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
			Depense += 5; //캐릭터 방어력 5 상승
			StartCoroutine("Skill1_3");
		}
	}


	IEnumerator Skill1_3()
	{

		yield return new WaitForSeconds(5.0f);
		Depense -= 5; //캐릭터 방어력 원상태로 돌리기
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
		Instantiate(skill2_meteo, transform.position + new Vector3(0, 0, 10), Quaternion.Euler(-90, 0, 0));
		isfight = false;
		yield return new WaitForSeconds(10.0f);
		isSkill2_4 = true;

	}

	public void skill3_2()
	{
		if (Input.GetKeyDown(KeyCode.Alpha2) && isSkill3_2 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false) //에임 조준 안했을시 2번 누르면 실행

		{
			anim.SetBool("Skill_1_Magic", true);
			isSkill3_2 = false;
			isfight = true;
			StartCoroutine("Skill3_2");


		}
	}

	IEnumerator Skill3_2()
	{

		yield return new WaitForSeconds(2.0f);
		anim.SetBool("Skill_1_Magic", false);
		Instantiate(skill3_wall, transform.position + this.transform.forward * 5, Quaternion.Euler(0, 0, 0));
		isfight = false;
		yield return new WaitForSeconds(5.0f); //스킬 쿨
		isSkill3_2 = true;

	}

	public void skill3_3()
	{
		if (Input.GetKeyDown(KeyCode.Alpha3) && isSkill3_3 == true && roll_check == false
			&& grounded == true && ShootSimple_Scr.SkillMode == false)
		{

			isSkill3_3 = false;
			skill3_buff.SetActive(true); //버프 이펙트 나오게하기			
			StartCoroutine("Skill3_3");
		}
	}

	IEnumerator Skill3_3()
	{

		yield return new WaitForSeconds(5.0f); //초후 버프 없애기
		skill3_buff.SetActive(false);
		yield return new WaitForSeconds(8.0f); //스킬 쿨타임
		isSkill3_3 = true;
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

		else if (Input.GetKeyDown(KeyCode.LeftShift) && isfight == false && run < 0 && grounded == true)
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
			anim.SetBool("isFight", true);
			weapon2.SetActive(true);
			//weapon1.SetActive(false);
			crosshair.SetActive(true);
			Debug.Log("조준 활성화");
		}
		else if (Input.GetMouseButtonUp(1) && isfight == true && roll_check == false)
		{
			Debug.Log("조준 끝");
			ShootSimple_Scr.SkillMode = false;
			//weapon3.SetActive(false);
			//ShootSimple_Scr.SkillMode = false;
			isfight = false;
			anim.SetBool("isFight", false);
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
			//Death();

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
			fightModUI.SetActive(true);
		else
			fightModUI.SetActive(false);
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

	/*public void Death()
	{
		Destroy(gameObject);
		Instantiate(ragdoll, transform.position, transform.rotation);
		dead = true;
	}*/

	public void PlayerD(float damage)
	{
		//데미지만큼 체력 감소
		MainCharHP -= damage; // health = health - damage;

		//체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
		/*if (MainCharHP <= 0 && !dead)
		{
			Death();
		}*/
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