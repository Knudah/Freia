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
	private int transparency;
	private Tools.Regulated regulated;
	private bool highlighted;
	private Button geneButton;
	private ColorBlock geneBlock;
	private Color normalColor = new Color32(255, 255, 255, 0);
	private Color normalHighlightColor = new Color32(255, 255, 0, 100);
	private Color highlightColor = new Color32(100, 100, 0, 150);
	private Color pathColor = new Color32(0, 0, 255, 150);

	
	public void OnMouseDown() {
		switch (highlighted) {
			case true:
				clearHighlight();
				break;
			case false:
				Highlight();
				break;
		}
	}

	public void Path() {
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;
		geneBlock.normalColor = pathColor;
		geneButton.colors = geneBlock;
	}

	public void Colorise() {
		geneBlock.highlightedColor = highlightColor;
		geneBlock.pressedColor = normalColor;
		switch(regulated)
		{
			case Tools.Regulated.unknown:
				geneBlock.normalColor = normalColor;
				break;
			case Tools.Regulated.up:
				geneBlock.normalColor = new Color32(255, 0, 0, (byte)transparency);
				break;
			case Tools.Regulated.down:
				geneBlock.normalColor = new Color32(255, 0, 255, (byte)transparency);
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
		Colorise ();
		highlighted = false;
	}

	public void SetTransparency(int value){
		transparency = value;
	}

	public void Start() {
		geneButton = gameObject.GetComponent<Button>();
		geneBlock = geneButton.colors;
		highlighted = false;
		regulated = Tools.RandomRegulated ();
	}
}