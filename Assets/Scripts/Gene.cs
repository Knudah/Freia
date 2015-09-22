using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gene : MonoBehaviour
{
	public string name;
	public string description;
	public int id;
	public int xPosition;
	public int yPosition;
	public int width;
	public int height;
	public List<int> edges;

	public void OnMouseDown() {
		Debug.Log ("Clicked on gene: " + name + " id: " + id);
	}

	public void colorize() {
	
	}

	public void highlight() {
	
	}

	public void clearHighlight() {
	
	}

	public void Start() {

	}
}