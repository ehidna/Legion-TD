using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector]
	public float damage;
	[HideInInspector]
	public float speed = 70f;

	[SerializeField]
	Transform destroyPlace;
	private Transform target;

	FighterStats stat;

	public void Seek(Transform _target){
		target = _target;
	}

	void Start(){
		stat = target.GetComponent<FighterStats> ();
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

		if (target.CompareTag ("Tower"))
			Debug.Log (hp);
		if (hp <= 0) {
			target.position = new Vector3 (destroyPlace.position.x, destroyPlace.position.y, destroyPlace.position.z);
			Destroy (target.gameObject, 0.1f);
		}
		Destroy(gameObject);
	}
}
