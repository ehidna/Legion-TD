using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour {

	public static Touch instance;

	public GameObject tile{ get; set;}
	public bool busy = false;

	void Start (){
		if (instance != null){
			Debug.Log("More than one Touch in scene!");
			return;
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (busy)
			return;
		if(Input.GetMouseButtonDown(0)){
			// Check if the mouse was clicked over a UI element
			if(!EventSystem.current.IsPointerOverGameObject()){
				Debug.Log("Did not Click on the UI");
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					if (tile != null) {
						Node oldNode = tile.GetComponent<Node> ();
						oldNode.DestroyPlatform ();
					} 
					if (hit.collider.gameObject.CompareTag ("Tile")) {
						Node node = hit.collider.gameObject.GetComponent<Node> ();
						if (node.hit)
							return;
						tile = hit.collider.gameObject;
						node.PutCirclePlatform ();
					}
				}
			}
		}	
	}
}
