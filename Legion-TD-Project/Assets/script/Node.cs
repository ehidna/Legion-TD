using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public GameObject fighter;
	[SerializeField]
	private GameObject backupFighter; // When fighter destroyed replace to it
	public Vector3 positionOffset;

	public ResourceController resource;
	UIController ui;

	public bool hit = false;
	Color col;

	Renderer render;
	public GameObject circlePlatform;
	private GameObject temporaryPlatform;
	private Transform root;

	public void rebuildFighter(){
		Destroy (fighter);
		if (backupFighter != null) {
			fighter = (GameObject)Instantiate (backupFighter, transform.position + positionOffset, transform.rotation);
			fighter.GetComponent<FighterController> ().targetTag = "Enemy_" + root.tag;
			fighter.GetComponent<Fighter> ().playerName = root.tag;
		}
	}

	public GameObject getFighter(){
		return fighter;
	}

	void Start(){
		root = transform.root;
		resource = root.GetComponentInChildren<ResourceController>();
		//		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		ui = GameObject.Find ("UIManager").GetComponent<UIController>();			
		render = GetComponent<Renderer> ();
	}

	void Update(){
		if (fighter != null) {
			hit = false;
			col = Color.red;
		}
		else {
			hit = true;
			col = Color.green;
		}
	}

	public void CantBuild(){
		if (fighter != null){
			ui.CantBuild ();
			return;
		}
	}

	public void PutCirclePlatform(){
		Vector3 offset = new Vector3 (0, 1, -0.5f);
		temporaryPlatform = (GameObject)Instantiate (circlePlatform, transform.position + offset, circlePlatform.transform.rotation);
	}

	public void Upgrade(){
		Node node = Touch.instance.tile.GetComponent<Node> ();
		Debug.Log ("Upgrading");
		GameObject evolved = node.getFighter().GetComponent<Fighter> ().evolveable;
		if (evolved != null) {
			node.BuildFighter (evolved);
		} else {
			node.CantBuild ();
		}
		node.DestroyPlatform ();
	}

	public void Sell(){
		Debug.Log ("Selling");
		Node node = Touch.instance.tile.GetComponent<Node> ();
		GameObject _fighter = node.getFighter ();
		node.resource.SellTower (_fighter);
		node.resource.NumberEffect (_fighter.transform, "+", _fighter.GetComponent<FighterStats>().getCost(), Color.yellow);
		Destroy (_fighter);
		node.DestroyPlatform ();
	}

	public void DestroyPlatform(){
		Destroy (temporaryPlatform);
	}

	public void BuildFighter(GameObject fighterToBuild){
		if (!resource.EnoughMoney (fighterToBuild)) {
			ui.NoMoney ();
			return;
		}
		Destroy (fighter);
		positionOffset = new Vector3(0, fighterToBuild.transform.localScale.y, 0);
		fighter = (GameObject)Instantiate (fighterToBuild, transform.position + positionOffset, transform.rotation);
		backupFighter = fighterToBuild;
		fighter.GetComponent<FighterController> ().targetTag = "Enemy_" + root.tag;
		fighter.GetComponent<Fighter> ().playerName = root.tag;
		resource.BuyTower (fighter);
		resource.NumberEffect (fighter.transform, "-", fighter.GetComponent<FighterStats>().getCost(), Color.red);
	}

	void OnMouseOver(){
		if (transform.root.tag == "Player") {
			render.enabled = true;
			render.material.color = col;
		}
	}

	void OnMouseExit(){
		if (transform.root.tag == "Player") {
			render.enabled = false;
		}
	}
}
