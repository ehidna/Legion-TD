using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	public GameObject[] enemy;

	[SerializeField]
	private GameObject field;

	UIController ui;
	ResourceController resource;

	// Use this for initialization
	void Start () {
		if (HUD.instance == null) {
			Debug.LogError ("Freak out! hud null");
		}

		ui = GameObject.Find ("UIManager").GetComponent<UIController> ();
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
	}

	void OnMouseDown(){
		HUD.instance.disableButtons (0); // enable barrack disable others
	}

	public void EnemyColor(int index){
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
}
