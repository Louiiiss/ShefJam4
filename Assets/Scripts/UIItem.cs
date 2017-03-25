using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IDropHandler {

	public int XSlots;
	public int YSlots;
	GameObject Grid;

	// Use this for initialization
	void Start () {
		Grid = GameObject.Find ("Grid");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData data){
		Grid.GetComponent<Grid> ().CurrentObjSizeX = XSlots;
		Grid.GetComponent<Grid> ().CurrentObjSizeY = YSlots;
	}

	public void OnDrop(PointerEventData data){
		print ("Dropped");
	}

}
