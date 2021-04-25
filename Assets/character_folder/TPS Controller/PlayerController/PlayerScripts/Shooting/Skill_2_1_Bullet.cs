using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2_1_Bullet : MonoBehaviour
{
	[Header("Bullet speed")]
	public int Speed;
	[Header("Target collision effect")]
	Vector3 lastPos;
	public GameObject hitEffect;
	public GameObject hitBlood;

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
			GameObject h = Instantiate<GameObject>(hitEffect);
			GameObject g = Instantiate(hitBlood, transform.position, transform.rotation);
			Destroy(h, 2);
			Destroy(g, 1);
			Destroy(gameObject);

		}

		lastPos = transform.position;
	}
}
