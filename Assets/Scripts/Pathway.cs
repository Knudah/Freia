using UnityEngine;
using System.Collections;

public class Pathway : MonoBehaviour
{
	public string name;
	public string description;
	public int width;
	public int height;
	public int xPosition;
	public int yPosition;

	void Start() {
		
	}

	public void OnMouseDown() {
		Debug.Log ("Clicked on pathway: " + name);
	}
}

