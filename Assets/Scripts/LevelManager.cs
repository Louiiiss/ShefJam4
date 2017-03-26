﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	public void loadLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	
	}
	public void quitRequest() {
		Application.Quit ();

	}
	public void winLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 2);

	}
	public void loseLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 3);

	}
}
