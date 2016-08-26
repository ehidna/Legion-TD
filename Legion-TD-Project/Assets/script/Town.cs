using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	[SerializeField]
	private float workerCooldown;
	private float countdown;

	private bool spawning;
	public int lumberIncome;

	public int cooldownPrice;

	ResourceController resource;

	// Use this for initialization
	void Start () {
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
		countdown = workerCooldown;
	}

	// Update is called once per frame
	void Update () {
		if(countdown <= 0){
			resource.EmptyResource (gameObject);
			countdown = workerCooldown;
		}
		countdown -= 1.0f * Time.deltaTime;
	}

	void OnMouseDown(){
		HUD.instance.disableButtons (1); // enable town buttons disable others
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
}
