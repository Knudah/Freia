using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPathway : MonoBehaviour {
	public string pathwayId;
	public Image pathwayImage;

	public void Start() {
		Debug.Log ("PathwayStart");
		pathwayImage = gameObject.transform.GetComponent<Image>();
		Debug.Log("GetImage");
		GetPathwayImage ();
		Debug.Log("StartDone");
//		GetPathwayKGML ();
	}

	public void GetPathwayImage() {
		string url = "http://localhost:8080/public/pathways/" + pathwayId + ".png";
//		Debug.Log (url);
		WWW www = new WWW(url);
		StartCoroutine(WaitForImageRequest(www));
	}
	
	public void GetPathwayKGML() {
		string url = "http://localhost:8080/pathway/" + pathwayId + "/json";
		Debug.Log (url);
		WWW www = new WWW(url);
		StartCoroutine(WaitForKGMLRequest(www));
	}

	IEnumerator WaitForImageRequest(WWW www) {
		yield return www;
		Texture2D texture = new Texture2D (100, 100);
		// check for errors
		if (www.error == null) {
//			Debug.Log("WWW Ok!: " + www.texture);
			Debug.Log("LoadImageIntoTexture");
			www.LoadImageIntoTexture(texture);
			Debug.Log("GetComponent");
			gameObject.GetComponent<Image> ().sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			Debug.Log("Done");
		} else {
			Debug.Log("WWW Error: "+ www.error);
		} 
	}

	IEnumerator WaitForKGMLRequest(WWW www) {
		yield return www;
		// check for errors
		if (www.error == null) {
			Debug.Log("WWW Ok!: " + www.text);
			//			ParseJson(www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		} 
	}
}
