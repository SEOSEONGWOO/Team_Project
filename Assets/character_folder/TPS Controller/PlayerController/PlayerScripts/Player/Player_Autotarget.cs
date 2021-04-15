using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Autotarget : MonoBehaviour {

	[Header("Enemy tag")]
	public string EnemyTag;
	[Header("Car Distance")]
	public float distAutotarget;


	[Space(10)]
	[Header("Statistics")]
	public GameObject currEnemy;
	public GameObject[] enemys;
	public float enemyDist;


	Player_Scr pc;

	void Start()
	{
		pc = GetComponent<Player_Scr> ();
		currEnemy = null;
	}
		

	public void FindTarget()
	{
		if (pc.isfight == true) 
		{
			enemys = GameObject.FindGameObjectsWithTag (EnemyTag);

			if(currEnemy == null)
				CheckTarget ();
		} 
		else
		{
			if (currEnemy != null) 
			{
				LoseTarget ();
			}
		}
	}

	public void CheckTarget()
	{
		if (enemys.Length > 0) 
		{
			GameObject currentObject = null;
			float dist = Mathf.Infinity;

			foreach (GameObject go in enemys) 
			{
				enemyDist = Vector3.Distance (gameObject.transform.position, go.transform.position);

				if (currEnemy) 
				{
					if (enemyDist < dist && currEnemy != go.gameObject) 
					{
						currentObject = go;
						dist = enemyDist;
					}
				}
				else
				{
					if (enemyDist < dist) 
					{
						currentObject = go;
						dist = enemyDist;
					}
				}
			}
			currEnemy = currentObject.gameObject;
		}
	}

	public void LookOnTarget()
	{
		if (currEnemy != null) 
		{
			enemyDist = Vector3.Distance (gameObject.transform.position, currEnemy.transform.position);
			if (enemyDist <= distAutotarget && pc.isfight == true)
			{
				pc.targetPos.transform.position = currEnemy.transform.position;
			}
			else 
			{
				LoseTarget ();
			}
		}
		else 
		{
			LoseTarget ();
		}


	}

	void LoseTarget()
	{
		if (pc.isfight == false) 
		{
			pc.targetPos.position = pc.targetPosOld.position;
			currEnemy = null;
		}
	}
}
