using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindPathScript : MonoBehaviour {

	[SerializeField]
	private InputField fromThisInputField = null;
	[SerializeField]
	private InputField toThisInputField = null;
	public Transform pathwaysPanel;
	private string fromGene;
	private string toGene;
	private Transform fromGeneInPathway;
	private Transform toGeneInPathway;

	void Start () {
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
		submitEvent.AddListener(FindGenesInPathway);
		toThisInputField.onEndEdit = submitEvent;
		fromThisInputField.onEndEdit = submitEvent;
	}

	void FindGenesInPathway(string gene){
		fromGene = fromThisInputField.text.ToUpper();
		toGene = toThisInputField.text.ToUpper();
		if (fromGene == toGene) {
			Debug.Log ("Path to the same gene?");
		}
		foreach (Transform pathway in pathwaysPanel) {
			fromGeneInPathway = null;
			toGeneInPathway = null;
			foreach(Transform obj in pathway){
				if (obj.tag == "Gene Box"){
					if (obj.GetComponent<Gene>().description == fromGene) {
						fromGeneInPathway = obj;
					} else if (obj.GetComponent<Gene>().description == toGene) {
						toGeneInPathway = obj;
					}
				}
			}
			if (fromGeneInPathway != null && toGeneInPathway != null) {
				_FindPath(fromGeneInPathway, pathway);
			}
		}
	}

	bool _FindPath(Transform gene, Transform pathway){
		if (gene.GetComponent<Gene> ().edges.Count == 0) {
			return false;
		}
		Transform obj;
		foreach(int edge in gene.GetComponent<Gene>().edges){
			if (edge == toGeneInPathway.GetComponent<Gene>().id){
				obj = FindNextGene(gene, pathway, edge);
				obj.GetComponent<Gene>().Path();
				gene.GetComponent<Gene>().Path();
				return true;
			}

			obj = FindNextGene(gene, pathway, edge);
			if (obj != null) {
				if (_FindPath(obj, pathway)){
					gene.GetComponent<Gene>().Path();
					return true;
				}
			}
		}
		return false;
	}

	Transform FindNextGene(Transform gene, Transform pathway, int edge) {
		foreach(Transform obj in pathway){
			if (obj.tag == "Gene Box"){
				if (obj.GetComponent<Gene>().id == edge) {
					return obj;
				}
			}
		}
		return null;
	}	
}
