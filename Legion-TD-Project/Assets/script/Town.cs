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

	private Transform root;

	// Use this for initialization
	void Start () {
		root = transform.root;
		resource = root.GetChild(0).GetComponentInChildren<ResourceController>();
		countdown = workerCooldown;
		if(root.tag == "Player"){
			townUI = GameObject.Find ("TownUI");
			townUI.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		if(countdown <= 0){
			resource.EmptyIncome (gameObject);
			countdown = workerCooldown;
			resource.NumberEffect (transform, "+", lumberIncome, Color.green);
		}
		countdown -= 1.0f * Time.deltaTime;
	}

	void OnMouseDown(){
		townUI.SetActive(true);
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

	public void Close(){
		townUI.SetActive (false);
	}
}
