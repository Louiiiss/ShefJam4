using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionSpawner : MonoBehaviour {

	public GameObject Minion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn(){
		GameObject SpawnedMinion = Instantiate (Minion, this.transform.position, new Quaternion ());
		this.GetComponent<Image> ().enabled = false;
	}
}
