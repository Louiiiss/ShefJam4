using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
	public int maxHealth = 400;
	public int currentHealth = 400;

	public float healthBarLength;
	public float baseBarLength = Screen.width / 3;
	// Use this for initialization
	void Start () {
		healthBarLength = baseBarLength;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
	}

	void OnGUI() {
		GUI.Box (new Rect (20,20,healthBarLength, 20), currentHealth + "/" + maxHealth);
	}

	void AdjustCurrentHealth(int adj){
		currentHealth += adj;
		if (currentHealth < 0) {
			currentHealth = 0;
		}
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		if (currentHealth < 1) {
			currentHealth = 1;
		}

		healthBarLength = baseBarLength * (currentHealth / (float)maxHealth);
	}
}
