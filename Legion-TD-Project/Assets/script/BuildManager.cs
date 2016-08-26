using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake (){
		if (instance != null){
			Debug.Log("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}
	public GameObject fighterPrefab;
	public GameObject rect;
	private GameObject currentRect;

	void OnEnable(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		Enable (tiles, true, "Tile");
		GameObject[] fighters = GameObject.FindGameObjectsWithTag ("Tower");
		Enable (fighters, false, "Tower");
		currentRect = Instantiate(rect, Input.mousePosition, Quaternion.identity) as GameObject;
	}

	void OnDisable(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		Enable (tiles, false, "Tile");
		GameObject[] fighters = GameObject.FindGameObjectsWithTag ("Tower");
		Enable (fighters, true, "Tower");
		Destroy (currentRect);
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
					item.GetComponent<EnemySeeker> ().enabled = change;
					item.GetComponent<Collider> ().enabled = change;
				}
			}
		}
	}

	void Start (){
		fighterToBuild = fighterPrefab;
	}

	private GameObject fighterToBuild;

	public GameObject GetFighterToBuild (){
		return fighterToBuild;
	}

	void FixedUpdate(){
		Vector3 temp = Input.mousePosition;
		temp.z = 15f; // Set this to be the distance you want the object to be placed in front of the camera.
		currentRect.transform.position =  Camera.main.ScreenToWorldPoint(temp);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {

			if (hit.collider.gameObject.CompareTag("Tile")) {
				currentRect.GetComponent<Renderer> ().material.color = hit.transform.GetComponent<Node> ().color;
				Debug.DrawRay(ray.origin, ray.direction * 100, hit.transform.GetComponent<Node> ().color);
			}
		}else
			currentRect.GetComponent<Renderer> ().material.color = Color.red;
	}
}
