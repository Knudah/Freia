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

	public Transform pathway;
	private Transform pathwaysParent;

	void Start() {
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
	}

	public void SelectPathway() {
		Debug.Log ("Clicked on: " + name);
		Transform p = (Transform) Instantiate (pathway, pathwaysParent.position, pathwaysParent.rotation);
		p.transform.SetParent (pathwaysParent);
		p.GetComponentInChildren<LoadPathway> ().pathwayId = name;
	}
}

