﻿using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour {
	//Drag variables
	private Vector3 screenPoint;
	private Vector3 offset;

	//Scale variables
	Vector3 scaleMin = new Vector3(0.1f, 0.1f, 0);
	Vector3 scaleMax = new Vector3(0.8f, 0.8f, 0);
	private float sensitivityDistance = -0.5f;

	public void OnMouseDown(){ 
		transform.SetAsLastSibling(); //Since Unity draws from top to bottom in the hierarchy this is a simple way to draw this object at the top
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	public void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}


	public void OnMouseOver() {
		float scaleDirection = Input.GetAxis ("Mouse ScrollWheel") * sensitivityDistance;

		if (scaleDirection != 0) {
			transform.localScale += Vector3.one * scaleDirection;
			transform.localScale = Vector3.Max(transform.localScale, scaleMin);
			transform.localScale = Vector3.Min(transform.localScale, scaleMax);

		}

		if (Input.GetKey (KeyCode.Escape)) {
			Destroy(gameObject);
		}

		if (Input.GetKey (KeyCode.Backspace)) {
			for(int i = 0; i < transform.childCount; i++) {
				if (transform.GetChild(i).tag == "Gene Box") {
					transform.GetChild(i).GetComponent<Gene>().clearHighlight();
				}
			}
		}
	}
}
