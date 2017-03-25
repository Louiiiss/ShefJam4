using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderCameraMove : MonoBehaviour {

	private float speed = 5f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 tempPos = this.transform.position;
		if (Input.mousePosition.x < 10) {
			tempPos.x -= speed * Time.deltaTime;
		} else if (Input.mousePosition.x > Screen.width - 10) {
			tempPos.x += speed * Time.deltaTime;
		}

		if (Input.mousePosition.y < 10) {
			tempPos.y -= speed * Time.deltaTime;
		} else if (Input.mousePosition.y > Screen.height - 10) {
			tempPos.y += speed * Time.deltaTime;
		}
		tempPos.x = Mathf.Clamp (tempPos.x, 0f, 50f);
		tempPos.y = Mathf.Clamp (tempPos.y, 0f, 50f);
		this.transform.position = tempPos;

	}
}
