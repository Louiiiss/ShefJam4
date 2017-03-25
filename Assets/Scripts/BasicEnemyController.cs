using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {


	public float health = 100;
	private Rigidbody2D rb;
	private Vector3 knockbackDir;
	public bool disabled = false;
	private float knockbackSpeed = 10f;
	private float knockbackTime = 0.05f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

		rb.freezeRotation = true; // So THIS is how you lock rotation? NEATO
	}
	
	// Update is called once per frame
	void Update () {
	
		if (health <= 0) {
			Destroy(gameObject);
		}

		KnockbackCooldown();

	}


	// Method to deal damage to this entity. Called from contact with player weapon.
	public void DealDamage(float damage){
		// Should call "CalculateDamageAfterMods" here, then remove health equal to the result
		health -= damage;
	}


	// Probably need to be able to pass in a value for power, ie how much to knockback
	public void Knockback(Transform playerRef){

		Vector3 playerPos = playerRef.position - this.transform.position;

		knockbackDir = Quaternion.Euler(0, 0, (Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg)-90) * Vector3.down;
		knockbackDir.Normalize();
		rb.velocity = knockbackDir * knockbackSpeed;

		disabled = true;
	}

	// Function that should take a raw damage number, do some maths, and return the number after reductions
	public float CalculateDamageAfterMods(float rawDamage){
		// rawDamage * k -- k would be the formula used to calculate damage due to weakenesses/strengths of the specific enemy.
		float finalDamage = rawDamage;

		return finalDamage;
	}

	// Cooldown function for knockback
	void KnockbackCooldown(){
		if (disabled) {
			knockbackTime -= Time.deltaTime;
		}

		if (knockbackTime <= 0 ) {
			rb.velocity = Vector3.zero;
			knockbackTime = 0.05f;
			disabled = false;
		}
	}


	void CheckSight(){

	}

}
