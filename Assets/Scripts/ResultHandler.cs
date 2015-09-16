using UnityEngine;
using System.Collections;

public class ResultHandler : MonoBehaviour {
	public string pathwayId;
	public Transform pathway;
	private Transform pathwaysParent;

	public void Start(){
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
		gameObject.AddComponent <BoxCollider2D>();
		gameObject.GetComponent<BoxCollider2D> ().size = new Vector2(gameObject.transform.parent.GetComponent<RectTransform> ().rect.width, 45);
		gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (170, -20);
//		Debug.Log (gameObject.transform.parent.GetComponent<RectTransform> ().rect.width);
		//		Debug.Log(pathwayId);
	}

	public void OnMouseDown() {
		Transform p = (Transform) Instantiate (pathway, pathwaysParent.position, pathwaysParent.rotation);
		p.transform.SetParent (pathwaysParent);
		p.GetComponentInChildren<LoadPathway> ().pathwayId = pathwayId;

		for(int i = 0; i < transform.parent.childCount; i++) {
			Destroy(transform.parent.GetChild(i).transform.gameObject);
		}
	}

	public void Update(){ 
		if (Input.GetKey (KeyCode.Escape)) {
			for(int i = 0; i < transform.parent.childCount; i++) {
				Destroy(transform.parent.GetChild(i).transform.gameObject);
			}
		}
	}	
}
