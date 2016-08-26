using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	//	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float speed = 70f;

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
		float hp = stat.getHealth() - damage;
		stat.setHealth (hp);

		if (hp <= 0) {

			if (!target.CompareTag ("Tower")) {
				resource.FighterReward (target.gameObject);
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
