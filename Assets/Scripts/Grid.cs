using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour {

	public GameObject GridSlot;
	static int GridX = 50;
	static int GridY = 50;
	public GameObject[,] GridArray = new GameObject[GridX,GridY];
	private GameObject ThisSlot;
	public GameObject SelectedObject;
	public bool isSelected = false;

	public int CurrentObjSizeX = 0;
	public int CurrentObjSizeY = 0;

	// Use this for initialization
	void Start () {
		int x = 0;
		int y = 0;
		for (int i = 0; i < GridX; i++) {
			for (int j = 0; j < GridY; j++) {
				ThisSlot = Instantiate (GridSlot, new Vector3 (x, y), new Quaternion(0,0,0,0));
				ThisSlot.transform.SetParent(this.transform);
				ThisSlot.GetComponent<GridScript> ().IndexX = i;
				ThisSlot.GetComponent<GridScript> ().IndexY = j;
				GridArray [i, j] = ThisSlot;
				y += 1;
			}
			y = 0;
			x += 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)){
			print ("Click");
			ClearObject();
		}
	}

	void ClearObject(){
		CurrentObjSizeX = 0;
		CurrentObjSizeY = 0;
		this.GetComponentInChildren<GridScript> ().HighlightGrid ();
	}
}
