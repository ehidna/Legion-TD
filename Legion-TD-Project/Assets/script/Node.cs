using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	private GameObject fighter;
	public Vector3 positionOffset;

	[HideInInspector]
	public Color color;

	ResourceController resource;
	UIController ui;

	void Start(){
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		ui = GameObject.Find ("UIManager").GetComponent<UIController>();			
	}

	void Update(){
		if (fighter != null)
			color = Color.red;
		else
			color = Color.green;
	}

	void OnMouseDown(){
		if (fighter != null){
			ui.CantBuild ();
			//			Debug.Log("Can't build there! - TODO: Display on screen.");
			return;
		}

		GameObject fighterToBuild = BuildManager.instance.GetFighterToBuild();
		if (resource.Money < fighterToBuild.GetComponent<FighterStats> ().getCost ()) {
			ui.NoMoney ();
			//			Debug.Log("No Money! - TODO: Display on screen.");
			return;
		} else {
			fighter = (GameObject)Instantiate (fighterToBuild, transform.position + positionOffset, transform.rotation);
			fighter.GetComponent<FighterStats> ().setPosition (fighter.transform.position);
			resource.BuyTower (fighter);
		}

	}
}
