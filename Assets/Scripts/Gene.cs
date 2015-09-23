using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gene : MonoBehaviour
{
	public string name;
	public string description;
	public int id;
	public int xPosition;
	public int yPosition;
	public int width;
	public int height;
	public List<int> edges;
	private bool enlighted;
	private Button geneButton;
	private ColorBlock geneBlock;
	private Color normalColor = new Color32(255, 255, 255, 0);
	private Color normalHighlightColor = new Color32(255, 255, 0, 100);
	private Color highlightColor = new Color32(100, 100, 0, 150);


	public void OnMouseDown() {
		if (!enlighted) {
			Debug.Log("Highlight: " + name);
			highlight ();

		} else {
			Debug.Log("Clear highlight: " + name);
			clearHighlight ();
		}
	}

	public void colorize() {
	
	}

	private void highlight() {
		geneBlock.normalColor = normalHighlightColor;
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;

		geneButton.colors = geneBlock;
		enlighted = true;
	}

	public void clearHighlight() {
		geneBlock.normalColor = normalColor;
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;
		
		geneButton.colors = geneBlock;
		enlighted = false;
	}

	public void Start() {
		geneButton = gameObject.GetComponent<Button>();
		geneBlock = geneButton.colors;
		enlighted = false;
	}
}