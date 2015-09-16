using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour {
	//Drag variables
	private Vector3 screenPoint;
	private Vector3 offset;

	//Scale variables
	Vector3 scaleMin = new Vector3(0.5f, 0.5f, 0.5f);
	Vector3 scaleMax = new Vector3(10,10,10);
	private float minScale = -4;
	private float maxScale = -2;
	private Vector3 zDistance;
	private float sensitivityDistance = -7.5f;
	private float damping = 2.5f;

	public void OnMouseDown(){ 
		transform.SetAsLastSibling(); //Since Unity draws from top to bottom in the hierarchy this is a simple way to draw this object at the top
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}


	public void OnMouseDrag(){
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}


	public void OnMouseOver() {
		float scaleDirection = Input.GetAxis ("Mouse ScrollWheel") * sensitivityDistance;

		if (scaleDirection != 0) {
//			scaleDirection = Mathf.Clamp (scaleDirection, minScale, maxScale);
//			zDistance.z = Mathf.Lerp(transform.localPosition.z, scaleDirection, Time.deltaTime * damping);
//			transform.localPosition = zDistance;
//			Vector3 pos = transform.position;
//			pos.x = .2f;
//			pos.y = -.15f;
//			transform.position = pos;
			transform.localScale += Vector3.one * scaleDirection;
			transform.localScale = Vector3.Max(transform.localScale, scaleMin);
			transform.localScale = Vector3.Min(transform.localScale, scaleMax);

		}

		if (Input.GetKey (KeyCode.Escape)) {
			Destroy(gameObject);
		}
	}
}
