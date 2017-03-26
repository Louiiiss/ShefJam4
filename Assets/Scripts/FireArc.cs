using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArc : MonoBehaviour {

	private BossAI boss;
	private float bossArcDam;

	// Use this for initialization
	void Start () {
		boss = GameObject.Find("Enemy Level 1 Boss").GetComponent<BossAI>();
		bossArcDam = boss.getArcDamage() + Random.Range(2,15);

		Debug.Log (boss.getArcDamage());
	}

	// Update is called once per frame
	void Update () {


	}



	void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("player")){
			col.SendMessageUpwards("DealDamage", bossArcDam);
			Debug.Log (bossArcDam);
			Destroy (gameObject);
		}



	}


}