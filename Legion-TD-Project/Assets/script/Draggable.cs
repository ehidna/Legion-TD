using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public GameObject fighterToBuild;
	private GameObject temporary;
	[SerializeField]

	public void OnBeginDrag(PointerEventData data){
		temporary = Instantiate (fighterToBuild, transform.position, Quaternion.identity)as GameObject;
	}

	public void OnDrag(PointerEventData data){
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Node node = hit.transform.GetComponent<Node> ();
			if (hit.collider.gameObject.CompareTag("Tile")) {
				if (node.hit) {
					Vector3 positionOffset = new Vector3(0, fighterToBuild.transform.localScale.y, 0);
					temporary.transform.position = node.transform.position + positionOffset;
				} 
			}
		}
	}

	void OnDisable(){
		if(temporary != null)
			Destroy (temporary);	
	}

	public void OnEndDrag(PointerEventData data){
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
