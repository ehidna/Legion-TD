using UnityEngine;
using System.Collections;

public class ResetFighters : MonoBehaviour {

	Node node;
	GameObject[] tiles;

	void OnEnable(){
		ResetNodes ();
	}

	void ResetNodes(){
		tiles = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject tile in tiles) {
			node = tile.GetComponent<Node> ();
			node.setFighter ();
		}
	}
}
