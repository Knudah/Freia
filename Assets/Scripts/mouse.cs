using UnityEngine;
using System.Collections;

public class mouse : MonoBehaviour {

	private GameObject selectedObject;

	void Start () {
	
	}
	
	void FixedUpdate() {
		if (Input.GetMouseButtonDown (0)) {
//			Debug.Log ("Pressed left click");
			CastRay ();
			if (selectedObject != null && selectedObject.tag == "Pathway"){
//				Move obj = selectedObject.GetComponent<Move>();
//				obj.MoveObject();
			}
		} else if (Input.GetMouseButtonDown (1)) {
//			Debug.Log ("Pressed right click");
			CastRay();
			if (selectedObject != null && selectedObject.tag == "Pathway"){
//				Move obj = selectedObject.GetComponent<Move>();
//				obj.ScaleObject();
			}
		} else {
			selectedObject = null;
		}
	}
	
	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

		if (hit) {
			Debug.DrawLine (ray.origin, hit.point, Color.red, 5f, false);
			selectedObject = hit.collider.gameObject;
		} else {
			Debug.DrawLine(ray.origin, Input.mousePosition, Color.white, 5f, false);

		}
	}
}
