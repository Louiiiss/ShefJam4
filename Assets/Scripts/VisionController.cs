using UnityEngine;
using System.Collections;

public class VisionController : MonoBehaviour {

	//private CircleCollider2D visionRange;
	private bool playerInRange = false;
	private Vector3 playerLocation;
	private BasicEnemyAI aiController;
	// Test damage variable
	private float damage = 33;

	// Use this for initialization
	void Start () {
		aiController = this.GetComponentInParent<BasicEnemyAI>();

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(playerInRange);
		//Debug.Log(playerLocation);
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			playerInRange = true;
			Transform player = col.GetComponentInParent<Transform>();
			playerLocation = player.position;

			// If this enemy doesn't already have a target, then set one.
			if (!aiController.HasTarget()){
				// Dummy function to test damage dealing
				//col.SendMessageUpwards("TakeDamage", damage);
				aiController.SetTarget(player);
			}

		}
	}


	// Could probably implement a system whereby if the player leaves the aggro range, that the enemy moves to the last location
	// of the player. 
	void OnTriggerExit2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			playerInRange = false;
		}
	}

}
