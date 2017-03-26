using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public float maxHealth = 100;
	public float currentHealth = 100;

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

	// Method to deal damage to this entity. Called from contact with player weapon.
	public void DealDamage(float damage){
		// Should call "CalculateDamageAfterMods" here, then remove health equal to the result
		currentHealth -= damage;
	}
	void OnGUI() {
		GUI.Box (new Rect (20,20,healthBarLength, 20), currentHealth + "/" + maxHealth);
		GUI.Box (new Rect (18,18,baseBarLength+4, 24)," ");
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
