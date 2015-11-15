using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LitJson;

public class SearchbarGene : MonoBehaviour {
	
	[SerializeField]
	private InputField nameInputField = null;
	public Transform PathwayParent;
	
	private void Start() {
		// Add listener to catch the submit
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
		submitEvent.AddListener(SubmitSearch);
		nameInputField.onEndEdit = submitEvent;
	}
	
	private void SubmitSearch(string search) {
		foreach(Transform pathway in PathwayParent){
			foreach(Transform obj in pathway){
				if (obj.tag == "Gene Box"){
					if (obj.GetComponent<Gene>().description == search.ToUpper()){
						obj.GetComponent<Gene>().Highlight();
					}
				}
			}
		}
	}
}
