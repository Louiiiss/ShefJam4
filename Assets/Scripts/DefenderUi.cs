using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderUi : MonoBehaviour {

	public GameObject Menu;
	public GameObject Rooms;
	public GameObject Minions;
	public GameObject Traps;

	public void OpenRooms(){
		Menu.SetActive (false);
		Rooms.SetActive (true);
	}

	public void OpenMinions(){
		Menu.SetActive (false);
		Minions.SetActive (true);
	}

	public void OpenTraps(){
		Menu.SetActive (false);
		Traps.SetActive (true);
	}

	public void Back(){
		Menu.SetActive (true);
		Rooms.SetActive (false);
		Minions.SetActive (false);
		Traps.SetActive (false);
	}

}
