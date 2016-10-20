using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public GameObject fighterToBuild;
	private GameObject temporary;
	public Transform placeToDrop;

	ResourceController resource;
	UIController ui;
	Image image;

	void Start(){
		image = GetComponent<Image> ();
	}

	public void OnBeginDrag(PointerEventData data){
		Touch.instance.busy = true;
		temporary = Instantiate (fighterToBuild, transform.position, Quaternion.identity)as GameObject;
		image.enabled = false;
	}

	public void OnDrag(PointerEventData data){
		if (temporary == null) {
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Node node = hit.transform.GetComponent<Node> ();
			if (hit.collider.gameObject.CompareTag ("Tile") && hit.collider.transform.root.tag != "AI") {
				if (node.hit) {
					Vector3 positionOffset = new Vector3 (0, fighterToBuild.transform.localScale.y, 0);
					temporary.transform.position = node.transform.position + positionOffset;
					temporary.transform.localEulerAngles = new Vector3 (0, 180, 0);
				} 
			}
		}
	}

	void OnDisable(){
		image.enabled = true;
		if(temporary != null)
			Destroy (temporary);	
	}

	public void OnEndDrag(PointerEventData data){
		Touch.instance.busy = false;
		image.enabled = true;
		if (temporary == null) {
			return;
		}
		Destroy (temporary);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Node node = hit.transform.GetComponent<Node> ();
			if (hit.collider.gameObject.CompareTag("Tile") && hit.collider.transform.root.tag != "AI") {
				if (node.hit) {
					node.BuildFighter (fighterToBuild);
				}
			}
		}
	}
}
