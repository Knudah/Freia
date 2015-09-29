using UnityEngine;
using System.Collections;
using System;
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
		gameObject.transform.localScale = new Vector2 (0.6f, 0.6f);
		pathwayImage = gameObject.transform.GetComponent<Image>();
		GetPathwayKGML ();
		GetPathwayImage ();
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
			www.LoadImageIntoTexture(texture);
			gameObject.GetComponent<Image> ().sprite = Sprite.Create(texture, new Rect(0,0, width, height), new Vector2(0.5f, 0.5f));
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

		//Retrieve image structures from JSON
		for (int i = 0; i < objs[0].Count; i++) {
			if (objs[0][i]["shape"].ToString().Contains("roundrectangle")){
				createPathwayInformation(objs[0][i]);

			} else if (objs[0][i]["shape"].ToString().Contains("rectangle")){
				if (string.Compare(objs[0][i]["name"].ToString(), "bg") == 0){ //do not include the bg img
					continue;
				}
				createGeneInformation(objs[0][i]);

			} else if (objs[0][i]["shape"].ToString().Contains("circle")){
				// This is a compound, not used in current implementation
			} else {
				Debug.Log("Kvik has a new structure which is not handled yet.");
			}
		}

		//Retrieve interactions between the genes
		for (int i = 0; i < objs[1].Count; i++) {
			foreach(Transform obj in gameObject.transform){
				if (obj.tag == "Gene Box"){
					if (obj.GetComponent<Gene>().id.Equals((int) objs[1][i]["source"])){
						obj.GetComponent<Gene>().edges.Add((int) objs[1][i]["target"]);
					}
				}
			}
		}
	}

	private void createPathwayInformation(JsonData data) {
		Transform p = (Transform) Instantiate (pathwayBox, gameObject.transform.position, gameObject.transform.rotation);
		p.SetParent(gameObject.transform);
		p.gameObject.GetComponent<Pathway>().name = data["name"].ToString().Replace("path:", String.Empty);
		p.gameObject.GetComponent<Pathway>().description = data["description"].ToString();
		p.gameObject.GetComponent<Pathway>().xPosition = int.Parse(data["x"].ToString());
		p.gameObject.GetComponent<Pathway>().yPosition = int.Parse(data["y"].ToString());
		p.gameObject.GetComponent<Pathway>().width = int.Parse(data["width"].ToString());
		p.gameObject.GetComponent<Pathway>().height = int.Parse(data["height"].ToString());
		p.gameObject.transform.localScale = new Vector2(1, 1);
		p.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(int.Parse(data["width"].ToString()), int.Parse(data["height"].ToString()));
		p.gameObject.GetComponent<RectTransform> ().position = new Vector3 (0,0,0);
		p.gameObject.GetComponent<RectTransform> ().localPosition = new Vector2 (calculateX(int.Parse(data["x"].ToString()), int.Parse(data["width"].ToString())),
		                                                                         calculateY(int.Parse(data["y"].ToString()), int.Parse(data["height"].ToString())));
	}

	private int calculateX(int boxX, int boxWidth){
		return (-1 * width/2) + boxX - boxWidth / 2;
	}

	private int calculateY(int boxY, int boxHeight){
		return ((-1 * height/2) + boxY - boxHeight/2) *-1;
	}

	private void createGeneInformation(JsonData data){
		Transform g = (Transform)Instantiate (geneBox, gameObject.transform.position, gameObject.transform.rotation);
		g.SetParent (gameObject.transform);
		g.gameObject.GetComponent<Gene> ().name = data ["name"].ToString ();
		g.gameObject.GetComponent<Gene> ().description = data ["description"].ToString ();
		g.gameObject.GetComponent<Gene> ().xPosition = int.Parse (data ["x"].ToString ());
		g.gameObject.GetComponent<Gene> ().yPosition = int.Parse (data ["y"].ToString ());
		g.gameObject.GetComponent<Gene> ().width = int.Parse (data ["width"].ToString ());
		g.gameObject.GetComponent<Gene> ().height = int.Parse (data ["height"].ToString ());
		g.gameObject.GetComponent<Gene> ().id = int.Parse (data ["id"].ToString ());
		g.gameObject.transform.localScale = new Vector2 (1, 1);
		g.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (int.Parse (data ["width"].ToString ()), int.Parse (data ["height"].ToString ()));
		g.gameObject.GetComponent<RectTransform> ().position = new Vector3 (0, 0, 0);
		g.gameObject.GetComponent<RectTransform> ().localPosition = new Vector2 (calculateX (int.Parse (data ["x"].ToString ()), int.Parse (data ["width"].ToString ())),
		                                                                         calculateY (int.Parse (data ["y"].ToString ()), int.Parse (data ["height"].ToString ())));
	}
}
