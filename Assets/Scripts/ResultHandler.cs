using UnityEngine;
using System.Collections;

public class ResultHandler : MonoBehaviour {
	public string pathwayId;
	public Transform pathway;
	private Transform pathwaysParent;
	private ApplicationLogic AppLogic;

	public void Start() {
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
		Transform AppWindow = GameObject.FindGameObjectWithTag("Application Window").transform;
		AppLogic = AppWindow.GetComponent<ApplicationLogic>();
		CreateResultButton ();
	}

	public void OnMouseDown() {
		Transform p = (Transform) Instantiate(pathway, pathwaysParent.position, pathwaysParent.rotation);
		p.transform.SetParent(pathwaysParent);
		p.GetComponentInChildren<LoadPathway>().pathwayId = pathwayId;

		RemoveSearchResult ();
		AppLogic.TogglePathwaySearchMenu();
	}

	public void Update() { 
		if (Input.GetKey (KeyCode.Escape)) {
			RemoveSearchResult();
		}
	}

	void RemoveSearchResult(){
		foreach (Transform result in transform.parent) {
			Destroy(result.gameObject);
		}
	}

	void CreateResultButton() {
		gameObject.transform.localScale = new Vector2 (1, 1);
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(gameObject.transform.parent.GetComponent<RectTransform>().rect.width, 45);
		gameObject.AddComponent<BoxCollider2D>();
		gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.transform.GetComponent<RectTransform>().rect.width, gameObject.transform.GetComponent<RectTransform>().rect.height);
		gameObject.GetComponent<BoxCollider2D>().offset = new Vector2 (gameObject.transform.GetComponent<RectTransform>().rect.width/2, gameObject.transform.GetComponent<RectTransform>().rect.height/2 *-1);	
	}
}
