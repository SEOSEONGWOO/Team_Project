using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {
	[Header("Bullet speed")]
	public int Speed;
	[Header("Target collision effect")]
	Vector3 lastPos;
	public GameObject hitEffect;
	public GameObject hitBlood;

	[Header("Bullet damage")]
	public static int bulletDamage = 50;

	void Start ()
	{
		lastPos = transform.position;

	}
	

	void Update ()
	{
		transform.Translate (Vector3.forward * Speed * Time.deltaTime);

		RaycastHit hit;
		Debug.DrawLine (lastPos, transform.position);
		if (Physics.Linecast(lastPos, transform.position, out hit))
		{
			if ((hit.collider.tag == "Enemy") || (hit.collider.tag == "Damage"))
			{
				hit.transform.SendMessage("SetDamageAI");
				print(hit.transform.name);
				GameObject h = Instantiate<GameObject>(hitEffect);
				Instantiate(hitBlood,transform.position,transform.rotation);
				Destroy(h, 2);
				Destroy(gameObject);
			}
			else
			{
				GameObject h = Instantiate<GameObject>(hitEffect);
				h.transform.position = hit.point + hit.normal * 0.001f;
				h.transform.rotation = Quaternion.LookRotation(-hit.normal);
				Destroy(h, 2);
				Destroy(gameObject);
			}
		}

		lastPos = transform.position;
		}

	 void OnTriggerEnter(Collider other)
	{
		Debug.Log("맞음");
	}
}