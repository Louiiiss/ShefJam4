using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour {


	public GameObject projectile;

	private Transform thisTransform;

	private Vector3 projectileDist;

	private Vector3 projectileDir;
	public float projectileSpeed;
	private bool playerInRange = false; 
	public float fireCooldown;
	public float fireDelay = 0.25f;
	private bool aiming = false;
	private Vector3 playerPos;

	public Transform playerTransform; 

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		playerTransform = GameObject.FindGameObjectWithTag ("player").transform;

		projectileDist = new Vector3(0.1f,0,0);
	}
	
	// Update is called once per frame
	void Update () {

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
