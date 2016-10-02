using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	//	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float speed = 70f;

	public Transform target;
	private Fighter fighter;
	public void Seek(Transform _target){
		target = _target;
	}

	void Start(){
		fighter = transform.GetComponentInParent<Fighter> ();
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
		fighter.Damage (damage);

		if (fighter.hpCheck) {
			fighter.Dead (fighter.tag);
		}
		Destroy(gameObject);
	}
}
