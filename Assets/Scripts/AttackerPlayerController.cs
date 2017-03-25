using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AttackerPlayerController : MonoBehaviour {

	private Transform thisTransform;



	//properties
	public float speed;
	public float speedCopy;

	public Collider2D attackUp;
	public Collider2D attackDown;
	public Collider2D attackLeft;
	public Collider2D attackRight;

	private bool attacking = false;

	private int facingDirection = 0;
	private float attackCooldown = 0.1f;
	private float attackTimer = 0.25f;


	
	// Use this for initialization
	void Start () {
		thisTransform = transform;


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
		Debug.Log(facingDirection);
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

			//AttackNudge();

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
}
