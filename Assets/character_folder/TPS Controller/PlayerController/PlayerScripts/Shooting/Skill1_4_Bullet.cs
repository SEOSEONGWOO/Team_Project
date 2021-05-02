using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_4_Bullet : MonoBehaviour
{
	[Header("Bullet speed")]
	public int Speed;
	[Header("Target collision effect")]
	Vector3 lastPos;
	
	public GameObject hitEffect1;
	public GameObject hitEffect2;
	public GameObject hitEffect3;
	public GameObject hitEffect4;
	void Start()
	{
		lastPos = transform.position;
	}

	void Update()
	{
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);

		RaycastHit hit;
		Debug.DrawLine(lastPos, transform.position);
		if (Physics.Linecast(lastPos, transform.position, out hit))
		{

			//hit.transform.SendMessage("SetDamageAI");
			
			GameObject g1 = Instantiate<GameObject>(hitEffect1);
			GameObject g2 = Instantiate(hitEffect2, transform.position, transform.rotation);
			GameObject g3 = Instantiate(hitEffect3, transform.position, transform.rotation);
			GameObject g4 = Instantiate(hitEffect4, transform.position, transform.rotation);
			Destroy(g1, 1);
			Destroy(g2 , 1);
			Destroy(g3, 1);
			Destroy(g4, 1);
			Destroy(gameObject);

		}

		lastPos = transform.position;
	}
}
