using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRange : MonoBehaviour {


	//private CircleCollider2D visionRange;
	private bool playerInRange = false;
	private Vector3 playerLocation;
	private BossAI aiController;
	// Test damage variable
	private float damage = 33;

	// Use this for initialization
	void Start () {
		aiController = this.GetComponentInParent<BossAI>();

	}

	// Update is called once per frame
	void Update () {
		//Debug.Log(playerInRange);
		//Debug.Log(playerLocation);
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			playerInRange = true;
			this.transform.parent.gameObject.GetComponent<BossAI> ().playerRange = true;
			Transform player = col.GetComponentInParent<Transform>();
			playerLocation = player.position;

		}
	}


	// Could probably implement a system whereby if the player leaves the aggro range, that the enemy moves to the last location
	// of the player. 
	void OnTriggerExit2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			playerInRange = false;
			this.transform.parent.gameObject.GetComponent<BossAI> ().playerRange = false;
		}
	}

}

