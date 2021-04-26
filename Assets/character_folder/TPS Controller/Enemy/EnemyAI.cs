using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour {

	[Header("Health/damage parameters")]
	[Tooltip("Zombie health")]
	public int AI_health = 100;
	[Tooltip("Zombie damage")]
	public static int damage = 20;

	[Header("Setting visibility options")]
	[Tooltip("The tag to which the AI will react")]
	public string targetTag = "Player";
	[Tooltip("Number of rays (in the script, rays are multiplied by 2)")]
	public int rays = 20;
	[Tooltip("AI visibility range")]
	public int distance = 15;
	[Tooltip("AI viewing angle")]
	public float angle = 40;


	[Header("Beam placement")]
	public Vector3 offset;
	private Transform target;
	private NavMeshAgent agent;

	[Header("Distance check")]
	public float distToTarget;

	[Header("Ragdoll AI")]
	public GameObject ragdoll;

	float attackDist = 0.7f;
	[Header("Time after which AI becomes passive")]
	public float waitTime = 10;

	void Start () 
	{
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
		agent = GetComponent<NavMeshAgent> ();
	}

	bool GetRaycast(Vector3 dir)
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		Vector3 pos = transform.position + offset;
		if (Physics.Raycast (pos, dir, out hit, distance))
		{
			if(hit.transform == target)
			{
				result = true;
				Debug.DrawLine(pos, hit.point, Color.green);
			}
			else
			{
				Debug.DrawLine(pos, hit.point, Color.blue);
			}
		}
		else
		{
			Debug.DrawRay(pos, dir * distance, Color.red);
		}
		return result;
	}

	bool RayToScan () //We calculate
	{
		bool result = false;
		bool a = false;
		bool b = false;
		float j = 0;
		for (int i = 0; i < rays; i++)
		{
			var x = Mathf.Sin(j);
			var y = Mathf.Cos(j);

			j += angle * Mathf.Deg2Rad / rays;

			Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
			if(GetRaycast(dir)) a = true;

			if(x != 0) 
			{
				dir = transform.TransformDirection(new Vector3(-x, 0, y));
				if(GetRaycast(dir)) b = true;
			}
		}

		if(a || b) result = true;
		return result;
	}

	public void Update()
	{
		if (Vector3.Distance (transform.position, target.position) < distance)  
		{
			if (RayToScan ()) 
			{
				PursuitAI ();
			}
		}
		else 
		{
			PassiveAI ();
		}
	}
		
	public void PursuitAI ()
	{
		agent.enabled = true;
		agent.SetDestination (target.transform.position);
		distToTarget = Vector3.Distance (target.transform.position, transform.position); 
		if (distToTarget < attackDist)
		{
			if (Player_Scr.dead == true)
			{
				StartCoroutine (TimeDown ());
			}
			gameObject.GetComponent<Animator> ().SetBool ("Attack", true);

			if (Vector3.Distance (transform.position, target.position) > distance)
			{
				gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
				PassiveAI ();
			}
		}
		else if (distToTarget > attackDist)
		{
			gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
			agent.enabled = true; 
			gameObject.GetComponent<Animator> ().SetTrigger ("Run");
		} 
		else
		{
			gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
		}
			

	}
	public void PassiveAI()
	{
		gameObject.GetComponent<Animator> ().SetBool ("Attack", false);
		agent.enabled = false; 
		gameObject.GetComponent<Animator> ().SetTrigger ("Idle");
	}


	public  void SetDamageAI()
	{
		AI_health -= Bullet.bulletDamage;
		if (AI_health <= 0) 
		{
			DeathAI ();
		}
	}



    private void OnTriggerEnter(Collider other)
	{
		Debug.Log("맞음");
		if (other.tag == "Skill1")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}
		}

		if (other.tag == "skill1_2")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}

		}
		if (other.tag == "Skill1_4")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}
		}

		if (other.tag == "Skill2_1")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}
			
		}

		if (other.tag == "Skill2_3")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}

		}
		if (other.tag == "Skill2_4")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}

		}
		if (other.tag == "Skill3_1")
		{
			AI_health -= 500;
			if (AI_health <= 0)
			{
				DeathAI();
			}

		}



	}



    public void DeathAI()
	{
		gameObject.SetActive (false);
		Instantiate (ragdoll, transform.position, transform.rotation);
	}

	IEnumerator TimeDown()
	{
		yield return new WaitForSeconds (waitTime);
		PassiveAI ();
	}
 }