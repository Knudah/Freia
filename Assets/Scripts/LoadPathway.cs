using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class LoadPathway : MonoBehaviour {
	public string pathwayId;
	public Image pathwayImage;
	public Transform geneBox;
	public Transform pathwayBox;
	private int width;
	private int height;

	public void Start() {
//		Debug.Log ("PathwayStart");
		pathwayImage = gameObject.transform.GetComponent<Image>();
		GetPathwayKGML ();
		//		Debug.Log("GetImage");
		GetPathwayImage ();
//		Debug.Log("StartDone");

	}

	public void GetPathwayImage() {
		string url = "http://localhost:8080/public/pathways/" + pathwayId + ".png";
		Debug.Log (url);
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

		// check for errors
		if (www.error == null) {
			width = www.texture.width;
			height = www.texture.height;
			Texture2D texture = new Texture2D (width, height);
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			gameObject.AddComponent<BoxCollider2D>();
			gameObject.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
//			gameObject.transform.localScale = new Vector2(width/Screen.width, height/Screen.height);
//			Debug.Log("WWW Ok!: " + www.texture);
//			Debug.Log("LoadImageIntoTexture");
//			Debug.Log("YOLO"+www.texture.width + "," + www.texture.height);

			www.LoadImageIntoTexture(texture);
//			Debug.Log("GetComponent");
			gameObject.GetComponent<Image> ().sprite = Sprite.Create(texture, new Rect(0,0, width, height), new Vector2(0.5f, 0.5f));
//			Debug.Log("Done");
		} else {
			Debug.Log("WWW Error: "+ www.error);
		} 
	}

	IEnumerator WaitForKGMLRequest(WWW www) {
		yield return www;
		// check for errors
		if (www.error == null) {
			Debug.Log("WWW Ok!: " + www.text);
			ParseJson(www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		} 
	}

	private void ParseJson(string json) {
		JsonData objs = JsonMapper.ToObject (json);
		if (objs[0] == null) {
			Debug.Log ("Could not find result!");
			return;
		}

		Debug.Log (objs [0].Count);
		for (int i = 0; i < objs[0].Count; i++) {
			if (objs[0][i]["shape"].ToString().Contains("roundrectangle")){
				createPathwayInformation(objs[0][i]);
				Debug.Log("pathway");
			} else if (objs[0][i]["shape"].ToString().Contains("rectangle")){
				Debug.Log("gene");

			} else if (objs[0][i]["shape"].ToString().Contains("circle")){
				Debug.Log("compound");
			} else {
				Debug.Log("what am I?");
			}
		}

		for (int i = 0; i < objs[1].Count; i++) {
			gameObject.GetComponent<Pathway>().width = 360;
		}
	}

	private void createPathwayInformation(JsonData data) {
//		Transform cam = Camera.main.transform;
//		Vector3 cameraRelative = cam.InverseTransformPoint (transform.position);
		Debug.Log("Pathway: " + data["name"].ToString() + " pos: " + data["x"].ToString() + "," + data["y"].ToString() + " new pos: " + int.Parse(data["x"].ToString()) /100 + "," + int.Parse(data["y"].ToString())/100 );
		Transform p = (Transform) Instantiate (pathwayBox, gameObject.transform.position, gameObject.transform.rotation);
		p.SetParent(gameObject.transform);
		p.gameObject.GetComponent<Pathway>().name = data["name"].ToString();
		p.gameObject.GetComponent<Pathway>().description = data["description"].ToString();
		p.gameObject.GetComponent<Pathway>().xPosition = int.Parse(data["x"].ToString());
		p.gameObject.GetComponent<Pathway>().yPosition = int.Parse(data["y"].ToString());
		p.gameObject.GetComponent<Pathway>().width = int.Parse(data["width"].ToString());
		p.gameObject.GetComponent<Pathway>().height = int.Parse(data["height"].ToString());
		p.gameObject.AddComponent<BoxCollider2D>();
		p.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(int.Parse(data["width"].ToString()), int.Parse(data["height"].ToString()));
		p.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(int.Parse(data["x"].ToString()), int.Parse(data["y"].ToString()) *-1);
		p.gameObject.transform.localScale = new Vector2(1, 1);
		p.gameObject.AddComponent<AspectRatioFitter> ();
		p.gameObject.GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
		p.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = 0.5f;
		Debug.Log ("polizia: " + p.gameObject.transform.localScale);

	}

	private void createGeneInformation(JsonData data){
		Transform g = (Transform) Instantiate (geneBox, gameObject.transform.position, gameObject.transform.rotation);
		g.SetParent(gameObject.transform);
	}

}
