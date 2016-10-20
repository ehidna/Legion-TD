using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Start (){
		if (instance != null){
			Debug.Log("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	void OnEnable(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		Enable (tiles, true, "Tile");

		GameObject[] fighters = GameObject.FindGameObjectsWithTag ("Tower");
		foreach (GameObject item in fighters) {
			SphereCollider radiusCollider = item.GetComponentInChildren<SphereCollider> ();
			radiusCollider.enabled = false;
		}
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
					item.GetComponent<NavMeshObstacle> ().enabled = !change;
					if(item.GetComponent<NavMeshAgent> () != null)
						item.GetComponent<NavMeshAgent> ().enabled = change;
					item.GetComponent<Collider> ().enabled = change;

					SphereCollider radiusCollider = item.GetComponentInChildren<SphereCollider> ();
					FighterStats stats = item.GetComponent<FighterStats> ();
					radiusCollider.radius = stats.viewRadius;
					radiusCollider.enabled = change;
				}
			}
		}
	}
}
