using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	//	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float speed = 70f;

	public GameObject numberEffect;

	public Transform target;
	private FighterController fighter;

	FighterStats stat;
	ResourceController resource;

	public void Seek(Transform _target){
		target = _target;
	}

	void Start(){
		stat = target.GetComponent<FighterStats> ();
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		fighter = transform.GetComponentInParent<FighterController> ();
	}

	// Update is called once per frame
	void Update () {
		if (target == null){
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame){
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
	void HitTarget (){
		if(!target.GetComponent<Fighter>().healthBar.activeSelf)
			target.GetComponent<Fighter> ().healthBar.SetActive (true);
		float hp = stat.getHealth() - damage;
		stat.setHealth (hp);

		if (hp <= 0) {

			if (!target.CompareTag ("Tower")) {
				resource.FighterReward (target.gameObject);

				// Number effect
				Vector3 pos = target.position;
				pos.y += 1; 
				GameObject effect = Instantiate (numberEffect, pos, transform.rotation) as GameObject;
				TextMesh text = effect.GetComponent<TextMesh> ();
				text.color = Color.yellow;
				text.text  = "+" + stat.getGold();
				Destroy (effect, 1);
			}
			TriggerExit ();
			Destroy (target.gameObject);
		}
		Destroy(gameObject);
	}
	void TriggerExit(){ // fire all enemies to remove target their list
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (fighter.tag);
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<FighterController> ().ColliderExit (target.GetComponent<Collider>());
		}
	}
}
