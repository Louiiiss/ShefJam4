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
	bool HasObject = false;


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
		if (Grid.GetComponent<Grid> ().isSelected && DoesFit()) {
			GameObject PlacedObject = Instantiate (Grid.GetComponent<Grid> ().SelectedObject, this.transform.position, new Quaternion (0, 0, 0, 0));
			PlacedObject.transform.SetParent (this.transform);
			PlacedObject.transform.position = (GridArray [IndexX, IndexY].transform.position + GridArray [IndexX + x-1, IndexY + y-1].transform.position)/2;
			PlacedObject.transform.localScale = new Vector3 (x/50f, y/50f, 1);
			PlacedObject.GetComponent<UIItem> ().enabled = false;
			Grid.GetComponent<Grid> ().ClearObject ();
			for (int i = 0; i < x; i++) {
				for (int j = 0; j < y; j++) {
					GridArray [IndexX + i, IndexY + j].GetComponent<GridScript> ().HasObject = true;
				}
			}
		}
	}

	bool DoesFit(){
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				if (GridArray [IndexX + i, IndexY + j].GetComponent<GridScript> ().HasObject == true) {
					return false;
				}
			}
		}
		return true;
	}


}
