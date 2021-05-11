using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_test : MonoBehaviour
{
	[Header("Bullet speed")]
	public int Speed;
	[Header("Target collision effect")]
	Vector3 lastPos;
	public GameObject hitEffect;
	public GameObject hitBlood;

	[Header("Bullet damage")]
	public static int bulletDamage = 50;

	public static bool isEnemy = false;

	void Start()
	{
		lastPos = transform.position;

	}
	public static string hitname;

	void Update()
	{
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);

		RaycastHit hit;
		Debug.DrawLine(lastPos, transform.position);
		if (Physics.Linecast(lastPos, transform.position, out hit))
		{
			StartCoroutine("DE");

			if ((hit.collider.tag == "Enemy") || (hit.collider.tag == "Damage"))
			{
				isEnemy = true;
				Debug.Log(hit.transform.gameObject.name);
				hitname = hit.transform.gameObject.name;
				//hit.transform.SendMessage("SetDamageAI");
				GameObject h = Instantiate<GameObject>(hitEffect);
				Instantiate(hitBlood, transform.position, transform.rotation);
				Destroy(h, 2);
				Destroy(gameObject);
			}
			else
			{
				GameObject h = Instantiate<GameObject>(hitEffect);
				h.transform.position = hit.point + hit.normal * 0.001f;
				h.transform.rotation = Quaternion.LookRotation(-hit.normal);
				//Instantiate(hitBlood, transform.position, transform.rotation);
				Destroy(h, 2);
				Destroy(gameObject);
			}
		}

		lastPos = transform.position;
	}

	IEnumerator DE()
	{
		yield return new WaitForSeconds(0.5f);


	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
	}
}
