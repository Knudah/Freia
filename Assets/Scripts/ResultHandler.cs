using UnityEngine;
using System.Collections;

public class ResultHandler : MonoBehaviour {
	public string pathwayId;
	public Transform pathway;
	private Transform pathwaysParent;

	public void Start(){
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
		gameObject.AddComponent <BoxCollider2D>();
//		gameObject.GetComponent<BoxCollider2D>().size = Renderer
//		Debug.Log(pathwayId);
	}

	public void OnMouseOver() {
		Debug.Log("MOUSEOVER: " + pathwayId);
	}

	public void Update(){ 
		if (Input.GetMouseButtonDown (0)) {
//			Debug.Log("CLICKED: " + pathwayId);
			Transform p = (Transform) Instantiate (pathway, pathwaysParent.position, pathwaysParent.rotation);
			p.transform.SetParent (pathwaysParent);
			p.GetComponentInChildren<LoadPathway> ().pathwayId = pathwayId;

			for(int i = 0; i < transform.parent.childCount; i++) {
				Destroy(transform.parent.GetChild(i).transform.gameObject);
			}
		}
	}	
}
