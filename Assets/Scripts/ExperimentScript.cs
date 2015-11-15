using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ExperimentScript : MonoBehaviour {

	public int numberOfPathways;
	public bool experimentTest;
	public bool performanceTest;
	public Transform pathway;
	public int testPathway;

	private Transform pathwayNumberText;
	private int numberPathway = 0;
	private LoadPathway lp;
	private Transform pathwaysParent;
	private List <string> pathwayList;
	private static float startT;

	void Start() {
		if (!experimentTest) {
			Destroy(gameObject);
			return;
		}
		pathwayList = new List<string> ();
		pathwaysParent = GameObject.FindGameObjectWithTag ("PathwayParent").transform;

		if (performanceTest) {
			pathwayNumberText = gameObject.transform.GetChild(0);
			InvokeRepeating ("GetNewPathway", 0f, 0.5f);

			string url = "http://localhost:8080/search/a";
			WWW www = new WWW(url);
			StartCoroutine(WaitForRequest(www));

		} else {
			pathwayList.Add("hsa04630");
			pathwayList.Add("hsa04915");
			pathwayList.Add("hsa04151");
			pathwayList.Add("hsa05200");
			RunEvaluationTest();
		}
	}

	void GetNewPathway(){
		if (numberPathway >= numberOfPathways) {
			Debug.Log("The amount of pathways have been reached!");
			return;
		} else if (pathwayList.Count < 1) {
			return;
		}
		numberPathway++;
		pathwayNumberText.GetComponentInChildren<Text> ().text = numberPathway.ToString();

		Transform p = (Transform) Instantiate(pathway, pathwaysParent.position, pathwaysParent.rotation);
		p.transform.SetParent(pathwaysParent);
		p.GetComponentInChildren<LoadPathway> ().pathwayId = pathwayList[(numberPathway %pathwayList.Count)];

	}


	void RunEvaluationTest(){
		startT = Time.time;

		Transform p = (Transform) Instantiate(pathway, pathwaysParent.position, pathwaysParent.rotation);
		p.transform.SetParent(pathwaysParent);
		if (testPathway > pathwayList.Count -1){
			Debug.Log("Not supported test pathway, try a number between 0 and 3.");
		}
		p.GetComponentInChildren<LoadPathway> ().pathwayId = pathwayList[testPathway];

	}

	public static float GetStartTime(){
		return startT;
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

	private void ParseJson(string json) {
		JsonData test = JsonMapper.ToObject (json);
		if (test[0] == null) {
			Debug.Log ("Could not find result!");
			return;
		}
		
		for (int i = 0; i < test[0].Count; i++) {
			
			if(test[0][i]["id"].ToString().Contains("hsa")){
				pathwayList.Add(test[0][i]["id"].ToString());
			}
		}
	}

}