using UnityEngine;
using System.Collections;

public class RangedVisionController : MonoBehaviour {

	//private CircleCollider2D visionRange;
	private bool playerInRange = false;
	private Vector3 playerLocation;
	private RangedEnemyController rangedController;
	// Test damage variable
	private float damage = 33;

	// Use this for initialization
	void Start () {
		rangedController = this.GetComponentInParent<RangedEnemyController>();

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(playerInRange);
		//Debug.Log(playerLocation);
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			rangedController.PlayerInRange(true);
		}
	}


	// Could probably implement a system whereby if the player leaves the aggro range, that the enemy moves to the last location
	// of the player. 
	void OnTriggerExit2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			rangedController.PlayerInRange(false);
		}
	}

}
