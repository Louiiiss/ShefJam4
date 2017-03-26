using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

	public Camera AttackerCamera;
	public Camera DefenderCamera;
	public GameObject Attacker;
	public GameObject Defender;
	public GameObject AttackerPlayer;

	bool AttackerTurn = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeTurns(){
		if (AttackerTurn) {
			MakeDefenderTurn ();
		} else { 
			MakeAttackerTurn ();
		}
		AttackerTurn = !AttackerTurn;
	}

	void MakeDefenderTurn(){
		Attacker.SetActive (false);
		Defender.SetActive (true);
		GameObject[,] GridArray = GameObject.Find ("Grid").GetComponent<Grid> ().GridArray;
		for (int i = 0; i < GridArray.GetLength (0); i++) {
			for (int j = 0; j < GridArray.GetLength (1); j++) {
				GridArray [i,j].GetComponent<Image>().enabled = true;
				GridArray [i,j].GetComponent<GridScript>().enabled = true;
			}
		}
	}

	void MakeAttackerTurn(){
		Defender.SetActive (false);
		Attacker.SetActive (true);
		GameObject[,] GridArray = GameObject.Find ("Grid").GetComponent<Grid> ().GridArray;
		for (int i = 0; i < GridArray.GetLength (0); i++) {
			for (int j = 0; j < GridArray.GetLength (1); j++) {
				GridArray [i,j].GetComponent<Image>().enabled = false;
				GridArray [i,j].GetComponent<GridScript>().enabled = false;
				if (!GridArray [i, j].GetComponent<GridScript> ().getHasRoom()) {
					GridArray [i, j].GetComponent<BoxCollider2D> ().enabled = true;
				}
			}
		}
		GameObject Player = Instantiate (AttackerPlayer, GameObject.FindGameObjectWithTag ("StartRoom").transform.position, new Quaternion ());
		Vector3 tempPos = Player.transform.position;
		tempPos.z = -0.1f;
		Player.transform.position = tempPos;
		tempPos.z = AttackerCamera.transform.position.z;
		AttackerCamera.transform.position = tempPos;
		AttackerCamera.GetComponent<CameraAttackFollower> ().Player = Player;
		GameObject[] Spawners = GameObject.FindGameObjectsWithTag ("Spawner");

		for(int i=0; i<Spawners.Length; i++){
			Spawners[i].GetComponent<MinionSpawner>().Spawn();
		}
	}
}
