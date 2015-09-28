using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplicationLogic : MonoBehaviour {
	private Transform pathwaysParent;
	public Transform Search_Pathway_UI;
	public Transform Search_Gene_UI;
	public Transform Expression_Gene_UI;
	public Transform Find_GenePath_UI;
	public Transform Gene_Expression_Slider;
	
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

	public void GeneExpressionValues() {
		int value = (int) Gene_Expression_Slider.GetComponent<Slider> ().value;
		foreach (Transform Pathway in pathwaysParent) {
			foreach(Transform obj in Pathway){
				if (obj.tag == "Gene Box"){
					obj.GetComponent<Gene>().SetTransparency(value);
					obj.GetComponent<Gene>().Colorise();
				}
			}
		}
	}


	public void TogglePathwaySearchMenu() {
		if (Search_Pathway_UI.gameObject.activeSelf) {
			Search_Pathway_UI.gameObject.SetActive(false);
			TogglePathwayCollider(true);
			Time.timeScale = 1.0f;
		} else {
			Search_Pathway_UI.gameObject.SetActive(true);
			Search_Gene_UI.gameObject.SetActive(false);
			Find_GenePath_UI.gameObject.SetActive(false);
			Expression_Gene_UI.gameObject.SetActive(false);
			TogglePathwayCollider(false);
			Time.timeScale = 0f;
		}
	}

	public void ToggleGeneSearchMenu(){
		if (Search_Gene_UI.gameObject.activeSelf) {
			Search_Gene_UI.gameObject.SetActive(false);
			TogglePathwayCollider(true);
			Time.timeScale = 1.0f;
		} else {
			Search_Gene_UI.gameObject.SetActive(true);
			Search_Pathway_UI.gameObject.SetActive(false);
			Find_GenePath_UI.gameObject.SetActive(false);
			Expression_Gene_UI.gameObject.SetActive(false);
			TogglePathwayCollider(false);
			Time.timeScale = 0f;
		}
	}

	public void ToggleGeneExpressionMenu(){
		if (Expression_Gene_UI.gameObject.activeSelf) {
			Expression_Gene_UI.gameObject.SetActive(false);
			TogglePathwayCollider(true);
			Time.timeScale = 1.0f;
		} else {
			Expression_Gene_UI.gameObject.SetActive(true);
			Search_Pathway_UI.gameObject.SetActive(false);
			Search_Gene_UI.gameObject.SetActive(false);
			Find_GenePath_UI.gameObject.SetActive(false);
			TogglePathwayCollider(false);
			Time.timeScale = 0f;
		}
	}
	
	public void ToggleGenePathMenu(){
		if (Find_GenePath_UI.gameObject.activeSelf) {
			Find_GenePath_UI.gameObject.SetActive(false);
			TogglePathwayCollider(true);
			Time.timeScale = 1.0f;
		} else {
			Find_GenePath_UI.gameObject.SetActive(true);
			Search_Pathway_UI.gameObject.SetActive(false);
			Search_Gene_UI.gameObject.SetActive(false);
			Expression_Gene_UI.gameObject.SetActive(false);
			TogglePathwayCollider(false);
			Time.timeScale = 0f;
		}
	}

	public void TogglePathwayCollider(bool active) {
		foreach(Transform child in pathwaysParent) {
			child.GetComponent<BoxCollider2D>().enabled = active;
		}
	}

}
