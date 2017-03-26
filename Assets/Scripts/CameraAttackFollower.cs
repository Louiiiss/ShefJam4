using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttackFollower : MonoBehaviour {

	public GameObject Player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tempPos = this.transform.position;
		tempPos.x = Player.transform.position.x;
		tempPos.y = Player.transform.position.y;
		this.transform.position = tempPos;
	}
}
