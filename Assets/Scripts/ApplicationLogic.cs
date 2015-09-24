using UnityEngine;
using System.Collections;

public class ApplicationLogic : MonoBehaviour {
	private Transform pathwaysParent;

	void Start() {
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
	}

	void Update () {
		//Remove all highlighting from all pathways
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey (KeyCode.Backspace)) {
			Debug.Log("Clear all highlight");
			foreach(Transform pathway in pathwaysParent) {
				foreach(Transform obj in pathway){
					if (obj.tag == "Gene Box"){
						obj.GetComponent<Gene>().clearHighlight();
					}
				}
			}
		}

		//Remove all pathways
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))  && Input.GetKey (KeyCode.Escape)) {
			Debug.Log("Clear all pathways");
			foreach(Transform child in pathwaysParent) {
				Destroy(child.gameObject);
			}
		}
	}
}
