using UnityEngine;
using System.Collections;

public class ApplicationLogic : MonoBehaviour {
	private Transform pathwaysParent;
	public Transform Search_UI;

	void Start() {
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
	}

	void Update () {
		ScanForKeyStrokes ();
	}

	void ScanForKeyStrokes(){
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


	public void ToggleSearchMenu() {
		// not the optimal way but for the sake of readability
		if (Search_UI.gameObject.activeSelf) {
			Search_UI.gameObject.SetActive(false);
			Time.timeScale = 1.0f;
		} else {
			Search_UI.gameObject.SetActive(true);
			Time.timeScale = 0f;
		}
		
		Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
	}

}
