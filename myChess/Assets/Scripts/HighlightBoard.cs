using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBoard : MonoBehaviour {
	public static HighlightBoard Instance{ set; get; }

	public GameObject highlighterPrefab;
	private List<GameObject> listHighlighter;

	private void Start()
	{
		Instance = this;
		listHighlighter = new List<GameObject> (); 
	}
		
	private GameObject getHighlight()
	{
		//this searches the list of pieces to find the first piece or object that matches to see if it is active.
		GameObject piece = listHighlighter.Find (g => !g.activeSelf);

		//in the scenario we don't find a piece that is active we will create a new instance of it and add it to the list of game objects.
		if (piece == null) {
			piece = Instantiate (highlighterPrefab);
			listHighlighter.Add (piece);
		}

		return piece;
	}

	public void highlightPossibleMoves(bool[,] moves)
	{
		for (int x = 0; x < 8; x++) {
			for (int y = 0; y < 8; y++) {
				if (moves [x, y]) {
					GameObject piece = getHighlight ();
					piece.SetActive(true);
					piece.transform.position = new Vector3 (x+0.5f, 0, y+0.5f); 
				}
			}
		}
	}

	//this is in the scenario we want to turn the possible highlighted moves off.
	public void hideTheHighlights()
	{
		foreach (GameObject piece in listHighlighter)
			piece.SetActive (false);
	}
}
