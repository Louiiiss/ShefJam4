using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class BasicEnemyAI : MonoBehaviour {

	public Transform target;

	// Rate of path update, per second
	public float updateRate = 2f;


	public float attackRange;
	private bool attacking = false;
	public float attackForce;
	public float attackDuration = 1f;
	public float attackCooldown = 2f;
	public bool canAttack = true;

	private Seeker seeker;
	private Rigidbody2D rb;
	private BasicEnemyController ec;

	// Calculated path
	public Path path;

	// Speed
	public float speed = 300f;
	public ForceMode2D fMode;
	public Vector3 dir;

	[HideInInspector]
	public bool pathIsEnded = false;

	// Max distance from the entity to a waypoint for it to continue
	public float nextWaypointDistance;

	// The waypoint we're currently moving towards
	private int currentWaypoint = 3;

	void Start(){

		ec = this.GetComponentInParent<BasicEnemyController>();
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

			
	}

	//Re-evaluate path to target. Calls itself every x seconds defined above
	IEnumerator UpdatePath(){
		if(target == null){
			// search for target
			yield return false;
		}

		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds(1/updateRate);

		StartCoroutine(UpdatePath());
	}


	public void OnPathComplete(Path p){
		Debug.Log("We've got a path. Did it have an error?" + p.error);
		if (!p.error){
			path = p;
			currentWaypoint = 0;
		}
	}


	void FixedUpdate(){

		Debug.Log("so this is doing something");

		if(attacking){
			attackLength();
		}

		reduceAttackCooldown();

		if (target == null){
			Debug.LogError("No player found");
			return;
		}

		if(path == null){
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count){
			if(pathIsEnded){
				return;
			}

			//Debug.Log("End of path rached");
			pathIsEnded = true;
			return;
		}

		pathIsEnded = false;

		// Direction to next waypoint

		if (!attacking){
			float distToTarget = Vector3.Distance(transform.position, target.position);

			//This is a bit of a hack atm. Basically gets another scripts variable. Probably bad practice.
			if (!ec.disabled){

				//Debug.Log("Waypoint position     " + path.vectorPath[currentWaypoint]); 
				//Debug.Log("Current position     " + transform.position); 

				//Debug.Log("Number of waypoints     " + path.vectorPath.Count);


				dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
				Debug.Log(dir);


				Debug.Log("Heading     " + dir); 

				dir = dir*speed*Time.fixedDeltaTime;

				rb.velocity = dir;
				// Move in the direction
				if (distToTarget<attackRange){
					rb.velocity = Vector3.zero;
					if(canAttack){
						attack();
					}
				}

			}

			float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

			Debug.Log("Vector3 Distance between this point and waypoint      " + dist);

			if(dist < nextWaypointDistance){
				currentWaypoint ++;
				return;
			}
		}

	}

	// Start the pathfinding, provided a target has been set.
	void BeginSeek(){
		if (target != null){
			seeker.StartPath(transform.position, target.position, OnPathComplete);

			StartCoroutine (UpdatePath());
		}
	}

	// Set the target
	public void SetTarget(Transform tg){
		target = tg;
		BeginSeek();
	}

	// Return whether this enemy has a target
	public bool HasTarget(){
		if (target == null){
			return false;
		}
		else {
			return true;
		}
	}


	public void attack(){
		attackDuration = 0.5f;
		attacking = true;
		Vector3 direction = (target.position - transform.position).normalized;
		direction = direction*attackForce*Time.deltaTime;

		rb.velocity = direction;
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




// Enemies now navigate around obstacles correctly, but will sometimes get stuck on corners because the algorithm for
// finding the shortest path doesn't consider the size of the object. 