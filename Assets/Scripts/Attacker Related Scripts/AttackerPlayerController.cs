using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AttackerPlayerController : MonoBehaviour {

	private Transform thisTransform;
	private Rigidbody2D rb;



	//properties
	public float speed;
	public float speedCopy;

	private float LSangle;

	public Collider2D attackUp;
	public Collider2D attackDown;
	public Collider2D attackLeft;
	public Collider2D attackRight;

	private bool attacking = false;
	private bool attackNudge = false;
	private float nudgeSpeed = 10f;
	private float nudgeTime = 0.05f;

	private int facingDirection = 0;
	private float attackCooldown = 0.1f;
	private float attackTimer = 0.25f;


	private float playerHealth = 100;
	private float weaponDamage = 40;


	
	// Use this for initialization
	void Start () {
		thisTransform = transform;
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;

		speedCopy = speed;


		attackUp.enabled = false;
		attackDown.enabled = false;
		attackLeft.enabled = false;
		attackRight.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();
		HandleMelee();
		AttackNudgeCooldown();
	}


	private void HandleMovement(){

		// Fine tune this at some point so that each movement has equal space on the thumbstick//
		if (CrossPlatformInputManager.GetAxis("LVertical")>0) {
			transform.position += Vector3.up * speed * Time.deltaTime * CrossPlatformInputManager.GetAxis("LVertical");
			facingDirection = 2;
		}
		if (CrossPlatformInputManager.GetAxis("LHorizontal")<0) {
			transform.position += Vector3.left * speed * Time.deltaTime * Mathf.Abs(CrossPlatformInputManager.GetAxis("LHorizontal"));
			facingDirection = 1;
		}
		if (CrossPlatformInputManager.GetAxis("LVertical")<0) {
			transform.position += Vector3.down * speed * Time.deltaTime * Mathf.Abs(CrossPlatformInputManager.GetAxis("LVertical"));
			facingDirection = 0;
		}
		if (CrossPlatformInputManager.GetAxis("LHorizontal")>0) {
			transform.position += Vector3.right * speed * Time.deltaTime * CrossPlatformInputManager.GetAxis("LHorizontal");
			facingDirection = 3;
		} 
	}




	private void HandleMelee(){
		//if (CrossPlatformInputManager.GetButtonDown("Melee") && !attacking && !aiming) {
		if (CrossPlatformInputManager.GetButtonDown("Melee") && !attacking) {
			
			//animator.SetBool("Attacking", true);
			Debug.Log("Pressed"); 

			if (facingDirection == 0){
				attackDown.enabled = true;
			}
			else if(facingDirection == 1){
				attackLeft.enabled = true;
			}
			else if(facingDirection == 2){
				attackUp.enabled = true;
			}
			else if(facingDirection == 3){
				attackRight.enabled = true;
			}
				
			speed = speed/4;

			attacking = true;

			AttackNudge();

		}

		if (attacking){

			if (attackTimer > 0){
				attackTimer -= Time.deltaTime;
			}
			else{
				attacking = false;
				speed = speedCopy;
			}
		}
		else {
			attackTimer = 0.25f;
			attackUp.enabled = false;
			attackDown.enabled = false;
			attackLeft.enabled = false;
			attackRight.enabled = false;
			//animator.SetBool("Attacking", false);
		}
	}



	// The little move when you attack
	void AttackNudge(){
		Vector3 ad = ResolveMovementVector();

		rb.velocity = ad * nudgeSpeed;
		attackNudge = true;
	}
	void AttackNudgeCooldown(){
		if (attackNudge) {
			nudgeTime -= Time.deltaTime;
		}

		if (nudgeTime <= 0 ) {
			Debug.Log("HERE");
			rb.velocity = Vector3.zero;
			nudgeTime = 0.05f;
			attackNudge = false;
		}
	}

	// Function to get the appropriate direction of movement. Defaults to "facing direction", else, the current movement angle
	Vector3 ResolveMovementVector(){
		// Left stick stuff
		// Dead Zone
		float angle;

		// Default Dashes based on facing direction
		if (facingDirection==0){
			angle = 0f;
		}
		else if(facingDirection ==1){
			angle = -90f;
		}
		else if(facingDirection ==2){
			angle = 180f;
		}
		else {
			angle = 90f;
		}

		/*
		// Advanced, adding full thumbstick range and 8 directional WASD
		if (CrossPlatformInputManager.GetButton("Up")&&CrossPlatformInputManager.GetButton("Left")) {
			angle = -135f;
		}
		else if(CrossPlatformInputManager.GetButton("Up")&&CrossPlatformInputManager.GetButton("Right")){
			angle = 135f;
		}
		else if(CrossPlatformInputManager.GetButton("Down")&&CrossPlatformInputManager.GetButton("Right")){
			angle = 45f;
		}
		else if(CrossPlatformInputManager.GetButton("Down")&&CrossPlatformInputManager.GetButton("Left")){
			angle = -45f;
		} */
			
		if(CrossPlatformInputManager.GetAxis("LHorizontal") > 0.3 || CrossPlatformInputManager.GetAxis("LVertical") > 0.3 || CrossPlatformInputManager.GetAxis("LHorizontal") < -0.3 || CrossPlatformInputManager.GetAxis("LVertical") < -0.3){
			LSangle = Mathf.Atan2(CrossPlatformInputManager.GetAxis("LHorizontal"), -CrossPlatformInputManager.GetAxis("LVertical")) * Mathf.Rad2Deg;
			angle = LSangle;
		}

		// Calculate the vector
		Vector3 mv = Quaternion.Euler(0, 0, angle-180) *  Vector3.up;

		// Return
		return mv;
	}



	public float getWeaponDamage(){
		return weaponDamage;
	}
}
