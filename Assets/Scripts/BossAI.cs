using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class BossAI : MonoBehaviour {
	public Transform target;
	public bool playerRange;
	public int moveSpeed;

	public float attackRange;
	private bool attacking = false;
	public float attackForce;
	public float attackSpeed;
	public float attackDuration = 1f;
	public float attackCooldown = 2f;
	public bool canAttack = true;

	private Transform myTransform;
	private float targetX;
	private float targetY;
	public Vector2 target2D;

	public GameObject projectile;
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
		if(!attacking) {
			Debug.Log (playerRange);
			targetX = target.position.x;
			targetY = target.position.y;
			target2D = new Vector2(targetX,targetY);
			myTransform.position = Vector2.MoveTowards(new Vector2(myTransform.position.x, myTransform.position.y), target2D, moveSpeed * Time.deltaTime);
			//myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			//Debug.Log (target2D);
			//Debug.Log (moveSpeed * Time.deltaTime);
			//Debug.Log (myTransform.position.y);
			if (playerRange){
				rb.velocity = Vector3.zero;
				if(canAttack){
					attack();
				}
			}
		}

		if(attacking){
			attackLength();
		}
	}

	void FixedUpdate() {


			
	}

	public void attack(){
		attackDuration = 0.5f;
		attacking = true;
		Vector3 direction = (target.position - transform.position).normalized;
		direction = direction*attackForce*Time.deltaTime;

		rb.velocity = direction;
		GameObject bullet = Instantiate(projectile, transform.position*attackSpeed, Quaternion.identity) as GameObject;
		bullet.GetComponent<Rigidbody>().AddForce(direction * 10);
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
}
