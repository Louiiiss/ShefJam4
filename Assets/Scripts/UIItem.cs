using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler {

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

	public void OnPointerClick(PointerEventData data){
		Grid.GetComponent<Grid> ().CurrentObjSizeX = XSlots;
		Grid.GetComponent<Grid> ().CurrentObjSizeY = YSlots;
		Grid.GetComponent<Grid> ().SelectedObject = this.gameObject;
		Grid.GetComponent<Grid> ().isSelected = true;
	}


}
