using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	public GameObject[] enemy;

	[SerializeField]
	private GameObject field;

	UIController ui;
	ResourceController resource;

	GameObject mercenaryUI;

	// Use this for initialization
	void Start () {
		mercenaryUI = GameObject.Find ("Mercenaries");
		ui = GameObject.Find ("UIManager").GetComponent<UIController> ();
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
		mercenaryUI.SetActive (false);
	}

	void OnMouseDown(){
		mercenaryUI.SetActive(true);
	}

	public void CreateMercenary(int index){
		if (resource.Lumber >= enemy[index].GetComponent<FighterStats> ().getCost ()) {
			Vector3 place = field.transform.position + Random.insideUnitSphere * 2;
			place.y = 0.3f;
			GameObject mercenary = Instantiate (enemy[index], place, Quaternion.identity) as GameObject;
			mercenary.GetComponent<FighterController> ().currentStatus = FighterController.enemyStatus.Idle;
			resource.BuyMercenaries (mercenary);
			mercenary.tag = "Mercenary";
		} else {
			ui.NoLumber ();
		}
	}

	public void Close(){
		mercenaryUI.SetActive(false);
	}
}
