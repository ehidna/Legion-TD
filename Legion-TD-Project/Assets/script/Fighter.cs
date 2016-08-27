using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	private float fireCountdown = 0f;
	public bool fight;

	//	[HideInInspector]
	public GameObject currentTarget;

	ArmorDamageTranslation adt;

	FighterStats stats;

	[SerializeField]
	GameObject bullet;

	[SerializeField]
	Transform firePoint;

	SphereCollider radiusCollider;

	public void setRadius(float rad){
		radiusCollider.radius = rad;
	}

	void Start () {
		radiusCollider = GetComponentInChildren<SphereCollider> ();
		stats = GetComponent<FighterStats> ();
		setRadius (stats.viewRadius);
		adt = GameObject.Find ("Translate").GetComponent<ArmorDamageTranslation> ();

	}

	void Update () {
		if (!GameManager.instance.building) 
			radiusCollider.enabled = true;

		if (currentTarget == null)
			return;

		Vector3 dir = currentTarget.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * stats.getTurnSpeed()).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
		if (!fight)
			return;

		if (fireCountdown <= 0f){
			Fight();
			fireCountdown = stats.getFireRate();
		}

		fireCountdown -= Time.deltaTime;
	}

	public void Fight(){
		GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
		Projectile _bullet = bulletGO.GetComponent<Projectile>();
		bulletGO.transform.SetParent (transform);

		if (_bullet != null ) {
			_bullet.speed = stats.getProjectileSpeed ();
			_bullet.damage =  adt.DamageReduction(stats.getDamageType(), currentTarget.GetComponent<FighterStats>().getArmorType(), stats.getDamage ());
			_bullet.Seek (currentTarget.transform);
		}
	}

}
