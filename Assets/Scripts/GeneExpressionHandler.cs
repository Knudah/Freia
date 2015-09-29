using UnityEngine;
using System.Collections;

public class GeneExpressionHandler : MonoBehaviour {
	private Transform pathwaysParent;

	// Use this for initialization
	void Start () {
		pathwaysParent = GameObject.FindWithTag("PathwayParent").transform;
	}
}
