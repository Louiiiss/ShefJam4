using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour {


	public GameObject projectile;

	private Transform thisTransform;

	private Vector3 projectileDist;

	public float health = 80;
	private Vector3 projectileDir;
	public float projectileSpeed;
	private bool playerInRange = false; 
	public float fireCooldown;
	public float fireDelay = 0.25f;
	private bool aiming = false;
	private Vector3 playerPos;

	private Rigidbody2D rb;
	private Vector3 knockbackDir;
	public bool disabled = false;
	private float knockbackSpeed = 10f;
	private float knockbackTime = 0.05f;

	public Transform playerTransform; 

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		playerTransform = GameObject.FindGameObjectWithTag ("player").transform;

		projectileDist = new Vector3(0.1f,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy(gameObject);
		}
		KnockbackCooldown();
		reduceFireCooldown();

		if(playerInRange && fireCooldown<=0 && !aiming){

			Aim();
		}

		if(aiming){
			fireDelay -= Time.deltaTime;
		}

		if(fireDelay<=0){
			Fire();
		}
	
	}

	// Probably need to be able to pass in a value for power, ie how much to knockback
	public void Knockback(Transform playerRef){

		Vector3 playerPos = playerRef.position - this.transform.position;

		knockbackDir = Quaternion.Euler(0, 0, (Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg)-90) * Vector3.down;
		knockbackDir.Normalize();
		rb.velocity = knockbackDir * knockbackSpeed;

		disabled = true;
	}

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



	// Method to deal damage to this entity. Called from contact with player weapon.
	public void DealDamage(float damage){
		// Should call "CalculateDamageAfterMods" here, then remove health equal to the result
		health -= damage;
	}

	private void Aim(){
		playerPos = playerTransform.position - thisTransform.position;

		projectileDir = Quaternion.Euler(0, 0, (Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg)-90)  * Vector3.up;
		projectileDir.Normalize();
		aiming = true;


	}

	private void Fire(){

		aiming = false;
		GameObject projectileObj = Instantiate(projectile,thisTransform.position + projectileDir, Quaternion.Euler(0, 0,(Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg)-90)) as GameObject;

		projectileObj.GetComponent<Rigidbody2D>().velocity = projectileDir * projectileSpeed;
		fireCooldown = 2f;
		fireDelay = 0.25f;
	}

	private void reduceFireCooldown(){
		fireCooldown -= Time.deltaTime;
	}


	public void PlayerInRange(bool truth){
		playerInRange = truth;
	}
}
