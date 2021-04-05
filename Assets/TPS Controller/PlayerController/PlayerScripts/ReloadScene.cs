﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.J))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}
