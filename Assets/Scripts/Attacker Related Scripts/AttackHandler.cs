using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour {

	private AttackerPlayerController player;
	private float damage;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject.GetComponent<AttackerPlayerController>();
		damage = player.getWeaponDamage();
	}
	
	// Update is called once per frame
	void Update () {


	}
		


	void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true && col.CompareTag("enemy")){
			col.SendMessageUpwards("DealDamage", damage);
			col.SendMessageUpwards("Knockback", player.transform);
		}
		if (col.isTrigger != true && col.CompareTag("Boss")){
			col.SendMessageUpwards("DealDamage", damage/2);
			col.SendMessageUpwards("Knockback", player.transform);
		}



	}
		

}
