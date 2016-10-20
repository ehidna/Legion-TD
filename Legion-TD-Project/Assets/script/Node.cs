using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	private GameObject fighter;
	[SerializeField]
	private GameObject backupFighter; // When fighter destroyed replace to it
	public Vector3 positionOffset;

	ResourceController resource;
	UIController ui;
	public bool hit = false;

	public void setFighter(){
		Destroy (fighter);
		if (backupFighter != null) {
			fighter = (GameObject)Instantiate (backupFighter, transform.position + positionOffset, transform.rotation);
		}
	}

	void Start(){
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		ui = GameObject.Find ("UIManager").GetComponent<UIController>();			
	}

	void Update(){
		if (fighter != null) {
			hit = false;
		}
		else {
			hit = true;
		}
	}

	public void CantBuild(){
		if (fighter != null){
			ui.CantBuild ();
			return;
		}
	}

	public void BuildFighter(GameObject fighterToBuild){
		positionOffset = new Vector3(0, fighterToBuild.transform.localScale.y, 0);
		fighter = (GameObject)Instantiate (fighterToBuild, transform.position + positionOffset, transform.rotation);
		backupFighter = fighterToBuild;
		resource.BuyTower (fighter);
	}
}
