using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	public GameObject[] enemy;

	[SerializeField]
	private GameObject field;

	UIController ui;
	ResourceController resource;
	GameObject mercenaries;

	private Transform root;

	void Start () {
		root = transform.root;
		resource = root.GetChild(0).GetComponentInChildren<ResourceController>();
		if (root.tag == "Player") {
			mercenaries = GameObject.Find ("Mercenaries");
			mercenaries.SetActive (false);
			ui = GameObject.Find ("UIManager").GetComponent<UIController> ();
		}
	}

	void OnMouseDown(){
		mercenaries.SetActive(true);
	}

	public void CreateMercenary(int index){
		if (resource.Lumber >= enemy[index].GetComponent<FighterStats> ().getCost ()) {
			Vector3 place = field.transform.position + Random.insideUnitSphere * 2;
			place.y = 0.3f;
			GameObject mercenary = Instantiate (enemy[index], place, Quaternion.identity) as GameObject;
			mercenary.GetComponent<FighterController> ().currentStatus = FighterController.enemyStatus.Idle;
			mercenary.GetComponent<Fighter> ().playerName = root.tag;
			resource.BuyMercenaries (mercenary);
			mercenary.tag = "Mercenary_" + root.tag;
		} else {
			ui.NoLumber ();
		}
	}

	public void Close(){
		mercenaries.SetActive (false);
	}
}
