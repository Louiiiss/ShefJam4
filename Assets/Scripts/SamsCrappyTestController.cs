using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamsCrappyTestController : MonoBehaviour {

	float speed = 5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = this.transform.position;
		if (Input.GetKey ("w")) {
			temp.y += speed * Time.deltaTime;
		}
		if (Input.GetKey ("s")) {
			temp.y -= speed * Time.deltaTime;
		}
		if (Input.GetKey("d")) {
			temp.x += speed * Time.deltaTime;
		}
		if (Input.GetKey ("a")) {
			temp.x -= speed * Time.deltaTime;
		}
		this.transform.position = temp;
			
	}
}
