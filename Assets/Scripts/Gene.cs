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
	public Tools.Regulated regulated;
	public List<int> edges;
	private bool highlighted;
	private Button geneButton;
	private ColorBlock geneBlock;
	private Color normalColor = new Color32(255, 255, 255, 0);
	private Color normalHighlightColor = new Color32(255, 255, 0, 100);
	private Color highlightColor = new Color32(100, 100, 0, 150);

	public void OnMouseDown() {
		if (!highlighted) {
			Debug.Log("Highlight: " + name);
			Highlight ();

		} else {
			Debug.Log("Clear highlight: " + name);
			clearHighlight ();
		}
	}

	public void Colorise(int value) {
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;
		switch(regulated)
		{
			case Tools.Regulated.unknown:
				break;
			case Tools.Regulated.up:
				geneBlock.normalColor = new Color32(255, 0, 0, (byte)value);
				break;
			case Tools.Regulated.down:
				geneBlock.normalColor = new Color32(255, 0, 255, (byte)value);
				break;
		}
		geneButton.colors = geneBlock;
	}

	public void Highlight() {
		geneBlock.normalColor = normalHighlightColor;
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;

		geneButton.colors = geneBlock;
		highlighted = true;
	}

	public void clearHighlight() {
		geneBlock.normalColor = normalColor;
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;
		
		geneButton.colors = geneBlock;
		highlighted = false;
	}

	public void Start() {
		geneButton = gameObject.GetComponent<Button>();
		geneBlock = geneButton.colors;
		highlighted = false;
		regulated = Tools.RandomRegulated ();
	}
}