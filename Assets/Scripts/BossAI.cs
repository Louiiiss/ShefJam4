using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class BossAI : MonoBehaviour {
	public Transform target;
	public bool playerRange;
	private float moveSpeed = 0.75f;

	public float attackRange;
	private bool attacking = false;
	public float attackForce;
	private float arcDamage = 5f;
	private float attackSpeed = 2f;
	public float attackDuration = 1f;
	public float attackCooldown = 2.5f;
	public bool canAttack = true;
	public float fireCooldown;
	public float fireDelay = 0.1f;
	private bool aiming = false;

	public float desiredDistance;
	private Transform myTransform;
	private float targetX;
	private float targetY;
	public Vector2 target2D;
	public Vector3 projectileDir;
	public Vector3 playerPos;

	public GameObject firearc;
	private Rigidbody2D rb;

	void Awake() {
		myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		GameObject playerTarget = GameObject.FindGameObjectWithTag ("player");
		target = playerTarget.transform;
		rb = GetComponent<Rigidbody2D>();
	
	}

	// Update is called once per frame
	void Update () {

		var offset  = this.transform.position - target.position;
		Debug.Log (offset.sqrMagnitude + " Offset magnitude");
		Debug.Log (desiredDistance);

		if(!attacking) {
			
			fireCooldown -= Time.deltaTime;
			Debug.Log (playerRange + "player range");
			targetX = target.position.x;
			targetY = target.position.y;
			target2D = new Vector2(targetX,targetY);
			myTransform.position = Vector2.MoveTowards(new Vector2(myTransform.position.x, myTransform.position.y), target2D, moveSpeed * Time.deltaTime);



			if (playerRange){
				rb.velocity = Vector3.zero;
				if(canAttack && fireCooldown<=0 && !aiming){
					Aim ();
					Debug.Log (aiming + "aiming");
					if(aiming){
						Debug.Log (fireDelay + "fire delay and");
						Debug.Log ("Attack");
						Attack();
					}
				}
			}
		}

		if(attacking){
			attackLength();
		}

	}

	void FixedUpdate() {


			
	}
	private void Aim(){
		playerPos = target.position - myTransform.position;
		projectileDir = Quaternion.Euler(0, 0, (Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg)-90)  * Vector3.up;
		projectileDir.Normalize();
		aiming = true;

	}

	public void Attack(){
		attackDuration = 0.5f;
		aiming = false;
		GameObject projectile = Instantiate(firearc,myTransform.position + projectileDir, Quaternion.Euler(0, 0,(Mathf.Atan2(playerPos.y,playerPos.x)*Mathf.Rad2Deg))) as GameObject;

		projectile.GetComponent<Rigidbody2D>().velocity = projectileDir * (attackSpeed);
		fireCooldown = 2.5f;
		fireDelay = 3.0f;
		attacking = false;
	}
	public void attackLength(){
		attackDuration -= Time.deltaTime;
		if(attackDuration<=0){
			rb.velocity = Vector3.zero;
			attacking = false;
			canAttack = false;
		}
	}

	public void reduceAttackCooldown(){
		attackCooldown -= Time.deltaTime;
		if (attackCooldown<=0){
			canAttack = true;
			attackCooldown = 2f;
		}
	}

	public float getArcDamage(){
		return arcDamage;
	}
}
