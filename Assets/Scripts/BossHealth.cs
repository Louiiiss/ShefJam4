using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour {
	public float maxHealth = 400;
	public float currentHealth = 400;

	public float healthBarLength;
	public float baseBarLength = Screen.width / 3;
	// Use this for initialization
	void Start () {
		healthBarLength = baseBarLength;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
		Debug.Log (currentHealth);
		if (currentHealth == 0.0f) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
	// Method to deal damage to this entity. Called from contact with player weapon.
	public void DealDamage(float damage){
		// Should call "CalculateDamageAfterMods" here, then remove health equal to the result
		currentHealth -= damage;
	}

	void OnGUI() {
		GUI.Box (new Rect (200,40,healthBarLength, 20), currentHealth + "/" + maxHealth);
		GUI.Box (new Rect (198,38,baseBarLength+4, 24)," ");
	}

	void AdjustCurrentHealth(int adj){
		currentHealth += adj;
		if (currentHealth < 0) {
			currentHealth = 0;
		}
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}

		healthBarLength = baseBarLength * (currentHealth / (float)maxHealth);
	}
}
