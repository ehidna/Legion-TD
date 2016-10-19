using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public GameObject fighterToBuild;
	private GameObject temporary;

	ResourceController resource;
	UIController ui;

	void Start(){
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		ui = GameObject.Find ("UIManager").GetComponent<UIController>();			
	}

	public void OnBeginDrag(PointerEventData data){
		if (resource.Money < fighterToBuild.GetComponent<FighterStats> ().getCost ()) {
			ui.NoMoney ();
			return;
		}
		temporary = Instantiate (fighterToBuild, transform.position, Quaternion.identity)as GameObject;
	}

	public void OnDrag(PointerEventData data){
		if (temporary == null) {
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Node node = hit.transform.GetComponent<Node> ();
			if (hit.collider.gameObject.CompareTag("Tile")) {
				if (node.hit) {
					Vector3 positionOffset = new Vector3(0, fighterToBuild.transform.localScale.y, 0);
					temporary.transform.position = node.transform.position + positionOffset;
					temporary.transform.localEulerAngles = new Vector3(0, 180, 0);
				} 
			}
		}
	}

	void OnDisable(){
		if(temporary != null)
			Destroy (temporary);	
	}

	public void OnEndDrag(PointerEventData data){
		if (temporary == null) {
			return;
		}
		Destroy (temporary);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Node node = hit.transform.GetComponent<Node> ();
			if (hit.collider.gameObject.CompareTag("Tile")) {
				if (node.hit) {
					node.BuildFighter (fighterToBuild);
				} else {
					node.CantBuild ();
				}
			}
		}
	}
}
