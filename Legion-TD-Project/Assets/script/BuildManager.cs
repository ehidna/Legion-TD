using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake (){
		if (instance != null){
			Debug.Log("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	void OnEnable(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		Enable (tiles, true, "Tile");
	}

	void OnDisable(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		Enable (tiles, false, "Tile");
		GameObject[] fighters = GameObject.FindGameObjectsWithTag ("Tower");
		Enable (fighters, true, "Tower");
	}

	void Enable(GameObject[] items, bool change, string tag){

		if (items.Length > 0) {
			foreach (GameObject item in items) {
				if (tag.CompareTo ("Tile") == 0) {
					item.GetComponent<Collider> ().enabled = change;
				}
				if (tag.CompareTo ("Tower") == 0) {
					item.GetComponent<FighterController> ().enabled = change;
					item.GetComponent<Fighter> ().enabled = change;
					item.GetComponent<NavMeshAgent> ().enabled = change;
					item.GetComponent<Collider> ().enabled = change;
				}
			}
		}
	}
		
}
