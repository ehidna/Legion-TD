using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	private GameObject fighter;
	[SerializeField]
	private GameObject backupFighter; // When fighter destroyed replace to it
	public Vector3 positionOffset;

	[HideInInspector]
	public Color color;

	ResourceController resource;
	UIController ui;

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
		if (fighter != null)
			color = Color.red;
		else
			color = Color.green;
	}

	void OnMouseDown(){
		if (EventSystem.current.IsPointerOverGameObject ()) // if in UI element simply return;
			return;

		if (fighter != null){
			ui.CantBuild ();
			return;
		}
		BuildManager.instance.Node = gameObject;
		HUD.instance.disableButtons (2);
	}

	public void BuildFighter(){
		GameObject fighterToBuild = BuildManager.instance.GetFighterToBuild();
		if (resource.Money < fighterToBuild.GetComponent<FighterStats> ().getCost ()) {
			ui.NoMoney ();
			return;
		} else {
			fighter = (GameObject)Instantiate (fighterToBuild, transform.position + positionOffset, transform.rotation);
			string name = fighter.name.Substring (0, fighter.name.Length - 7);
			backupFighter = Resources.Load(name)as GameObject;
			resource.BuyTower (fighter);
		}
		HUD.instance.disableButtons (-1); // disable all buttons
	}
}
