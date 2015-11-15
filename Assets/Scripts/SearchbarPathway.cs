using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LitJson;

public class SearchbarPathway : MonoBehaviour {

	[SerializeField]
	private InputField nameInputField = null;
	public Transform result;
	public Transform searchResult;

	private void Start() {
		// Add listener to catch the submit
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
		submitEvent.AddListener(SubmitSearch);
		nameInputField.onEndEdit = submitEvent;
	}
	
	private void SubmitSearch(string search) {
		string url = "http://localhost:8080/search/"+search;
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		// check for errors
		if (www.error == null) {
			ParseJson(www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		} 
	}

	private void CreateSearchResult(string resultName, string resultId) {
		Transform r = (Transform) Instantiate (result, searchResult.position, searchResult.rotation);
		r.SetParent (searchResult);
		r.GetComponentInChildren<Text> ().text = resultName;
		r.GetComponentInChildren<ResultHandler> ().pathwayId = resultId;
	}


	private void ParseJson(string json) {
		JsonData test = JsonMapper.ToObject (json);
		if (test[0] == null) {
			Debug.Log ("Could not find result!");
			return;
		}

		for (int i = 0; i < test[0].Count; i++) {

			if(test[0][i]["id"].ToString().Contains("hsa")){
				CreateSearchResult(test[0][i]["value"].ToString(), test[0][i]["id"].ToString());
			}
		}
	}
}
