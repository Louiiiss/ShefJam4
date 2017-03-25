using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {

	public Material NotHigh;
	public Material Highlighted;
	GameObject Grid;
	GameObject[,] GridArray;
	public int IndexX = 0;
	public int IndexY = 0;
	int x;
	int y;


	// Use this for initialization
	void Start () {
		Grid = this.transform.parent.gameObject;
		GridArray = Grid.GetComponent<Grid> ().GridArray;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData data){
		HighlightGrid ();
	}

	public void HighlightGrid(){
		for (int i = 0; i < GridArray.GetLength (0); i++) {
			for (int j = 0; j < GridArray.GetLength (1); j++) {
				GridArray [i,j].GetComponent<Image> ().material = NotHigh;
			}
		}
		x = Grid.GetComponent<Grid> ().CurrentObjSizeX;
		y = Grid.GetComponent<Grid> ().CurrentObjSizeY;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				GridArray [IndexX + i, IndexY + j].GetComponent<Image>().material = Highlighted;
			}
		}
	}

	public void OnPointerClick(PointerEventData data){
		if (Grid.GetComponent<Grid> ().isSelected) {
			
		}
	}


}
