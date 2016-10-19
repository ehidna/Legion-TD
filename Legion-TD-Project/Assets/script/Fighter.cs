using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	private float fireCountdown = 0f;
	public bool fight;

	public enum FighterType{Melee, Range};
	public FighterType currentType;

	//	[HideInInspector]
	public GameObject currentTarget;

	ArmorDamageTranslation adt;
	FighterAnimator anim;
	FighterStats stats;

	//	[SerializeField]
	public GameObject bullet;

	[SerializeField]
	Transform firePoint;

	public GameObject numberEffect;
	public GameObject muzzleFlash;
	public GameObject healthBar;
	public GameObject hitEffect;

	SphereCollider radiusCollider;
	ResourceController resource;

	public void setRadius(float rad){
		radiusCollider.radius = rad;
	}

	void Start () {
		radiusCollider = GetComponentInChildren<SphereCollider> ();
		stats = GetComponent<FighterStats> ();
		setRadius (stats.viewRadius);
		adt = GameObject.Find ("Translate").GetComponent<ArmorDamageTranslation> ();
		resource = GameObject.Find ("ResourceManager").GetComponent<ResourceController>();
		anim = GetComponent<FighterAnimator>();
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
			if (anim != null) 
				anim.StartPunch ();
			else
				Fight();

			fireCountdown = stats.getFireRate();
		}

		fireCountdown -= Time.deltaTime;
	}

	public void Fight(){
		if (currentTarget == null)
			return;

		if (currentType == FighterType.Melee) {
			Damage (adt.DamageReduction(stats.getDamageType(), currentTarget.GetComponent<FighterStats>().getArmorType(), stats.getDamage ()));

			if (hpCheck) {
				Dead (transform.tag);
			}
			if (anim != null) {
				anim.ExitPunch();
			}

		} else {
			GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
			Projectile _bullet = bulletGO.GetComponent<Projectile>();	
			if (muzzleFlash != null) {
				GameObject muzzle = (GameObject)Instantiate (muzzleFlash, firePoint.position, firePoint.rotation);
				muzzle.transform.SetParent (firePoint);
				Destroy (muzzle, 1f);
			}

			bulletGO.transform.SetParent (transform);

			if (_bullet != null) {
				_bullet.speed = stats.getProjectileSpeed ();
				_bullet.damage = adt.DamageReduction(stats.getDamageType(), currentTarget.GetComponent<FighterStats>().getArmorType(), stats.getDamage ());
				_bullet.Seek (currentTarget.transform);
			}
			if (anim != null) {
				anim.ExitPunch();
			}
		}

	}

	private float hp;
	public void Damage(float damage){
		if(!currentTarget.GetComponent<Fighter>().healthBar.activeSelf)
			currentTarget.GetComponent<Fighter> ().healthBar.SetActive (true);

		FighterStats stat = currentTarget.GetComponent<FighterStats> ();
		hp = stat.getHealth() - damage;
		stat.setHealth (hp);

		if (hitEffect != null) {
			GameObject hitE = (GameObject)Instantiate (hitEffect, currentTarget.transform.position, Quaternion.identity);
			Destroy (hitE, 1.5f);
		}
	}

	public bool hpCheck{get{ return hp <= 0; }}

	public void Dead(string tag){
		if (!currentTarget.CompareTag ("Tower")) {
			resource.FighterReward (currentTarget);
			// Number effect
			Vector3 pos = currentTarget.transform.position;
			pos.y += 1; 
			GameObject effect = Instantiate (numberEffect, pos, transform.rotation) as GameObject;
			TextMesh text = effect.GetComponent<TextMesh> ();
			text.color = Color.yellow;
			text.text  = "+" + currentTarget.GetComponent<FighterStats>().getGold();
			Destroy (effect, 1);
		}
		TriggerExit ();
		Destroy (currentTarget);
	}

	void TriggerExit(){ // fire all enemies to remove target their list
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (transform.tag);
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<FighterController> ().ColliderExit (currentTarget.GetComponent<Collider>());
		}
	}

}
