using UnityEngine;
using System.Collections;

public class Barrack : MonoBehaviour {

	private bool inside;

	[SerializeField]
	private Material[] materials;

	public GameObject enemy;

	[SerializeField]
	private GameObject field;

	[SerializeField]
	private LayerMask mask;

	UIController ui;
	ResourceController resource;

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

	// Update is called once per frame
	void Update () {
		if (!inside) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, mask.value)) {
				if (Input.GetMouseButtonDown (0)) {
					Debug.Log ("Disariya tikladin. "+ inside);
					HUD.instance.disableButtons ();
				}
			} else {
				if (hit.transform == HUD.instance.image) {
					inside = true;
					Debug.Log("Image'a tikladin. " + inside);
				}
			}
		}
	}
	// materialin son 2 harfini inte cevirme
	private int convertToPosition(string name){
		return int.Parse(name.Substring (5));
	}

	void OnMouseDown(){
		HUD.instance.disableButtons ();
		for (int i=0; i<materials.Length; i++) {
			Debug.Log (convertToPosition(materials[i].name).ToString());
			HUD.instance.setButtonImage (materials [i].color, convertToPosition(materials[i].name));
			HUD.instance.activateButton (convertToPosition(materials[i].name));
		}
	}

	public void EnemyColor(Material material){
		if (resource.Lumber >= enemy.GetComponent<FighterStats> ().getCost ()) {
			enemy.GetComponent<Renderer> ().material = material;
			Vector3 place = field.transform.position + Random.insideUnitSphere * 2;
			place.y = 0.3f;

			GameObject mercenary = Instantiate (enemy, place, Quaternion.identity) as GameObject;
			mercenary.GetComponent<FighterController> ().currentStatus = FighterController.enemyStatus.Idle;
			resource.BuyMercenaries (mercenary);
			mercenary.tag = "Mercenary";
		} else {
			ui.NoLumber ();
		}

	}

	void OnMouseOver(){
		inside = true;
	}

	void OnMouseExit(){
		inside = false;
	}

}
