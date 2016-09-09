using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	[SerializeField]
	GameObject numberEffect;

	[SerializeField]
	private float workerCooldown;
	private float countdown;

	private bool spawning;
	public int lumberIncome;

	public int cooldownPrice;

	ResourceController resource;

	GameObject townUI;

	// Use this for initialization
	void Start () {
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
		townUI = GameObject.Find ("TownUI");
		countdown = workerCooldown;
		townUI.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if(countdown <= 0){
			resource.EmptyResource (gameObject);
			countdown = workerCooldown;
			NumberEffect ();
		}
		countdown -= 1.0f * Time.deltaTime;
	}

	void OnMouseDown(){
		townUI.SetActive (true);
	}

	public void IncreaseLumber(int increase){
		if (resource.Money < 50)
			return;
		lumberIncome += increase;
		resource.BuyWorker (50);
	}

	public void DecreaseLumberCooldown(float time){
		if (resource.Money < cooldownPrice || resource.Lumber < cooldownPrice)
			return;

		resource.DecreaseCooldown (cooldownPrice);
		workerCooldown -= time;
		cooldownPrice += 20;
	}

	void NumberEffect(){
		Vector3 pos = transform.position;
		pos.y += 1; 
		GameObject effect = Instantiate (numberEffect, pos, transform.rotation) as GameObject;
		TextMesh text = effect.GetComponent<TextMesh> ();
		text.color = Color.green;
		text.text  = "+" + lumberIncome;
		Destroy (effect, 1);
	}

	public void Close(){
		townUI.SetActive (false);
	}
}
