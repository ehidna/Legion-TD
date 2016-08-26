using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	[SerializeField]
	private Material[] materials;

	public GameObject[] enemy;

	[SerializeField]
	private GameObject field;

	UIController ui;
	ResourceController resource;
	int index;

	// Use this for initialization
	void Start () {
		if (HUD.instance == null) {
			Debug.LogError ("Freak out! hud null");
		}
		if (materials == null)
			Debug.LogError ("Materialler bozukkkk.");

		ui = GameObject.Find ("UIManager").GetComponent<UIController> ();
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
	}

	void OnMouseDown(){
		HUD.instance.disableButtons (0); // enable barrack disable others
	}

	public void EnemyColor(Material material){
		if (resource.Lumber >= enemy[index].GetComponent<FighterStats> ().getCost ()) {
			enemy[index].GetComponent<Renderer> ().material = material;
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

	public void IndexofMercenary(int _index){
		index = _index;
	}
}
