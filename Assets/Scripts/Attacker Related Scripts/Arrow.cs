using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private float damage;

	// Use this for initialization
	void Start () {
		damage = 5 + Random.Range(2,15);
	}

	// Update is called once per frame
	void Update () {


	}



	void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			col.SendMessageUpwards("DealDamage", damage);
			Debug.Log (damage);
			Destroy (gameObject);
		}
	}

}

