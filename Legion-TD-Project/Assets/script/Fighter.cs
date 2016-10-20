using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	private float fireCountdown = 0f;
	public bool fight;
	public string playerName;

	public enum FighterType{Melee, Range};
	public FighterType currentType;

	ArmorDamageTranslation adt;
	FighterAnimator anim;
	FighterStats stats;

	public bool king = false;
	[SerializeField]
	Transform firePoint;

	public GameObject currentTarget;

	public GameObject bullet;
	public GameObject evolveable;

	public GameObject muzzleFlash;
	public GameObject healthBar;
	public GameObject hitEffect;

	ResourceController resource;

	void Start () {
		stats = GetComponent<FighterStats> ();
		adt = GameObject.Find ("Translate").GetComponent<ArmorDamageTranslation> ();
		resource = GameObject.Find (playerName).GetComponent<ResourceController>();
		anim = GetComponent<FighterAnimator>();
	}

	void Update () {
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
		Fighter fighter = currentTarget.GetComponent<Fighter> ();
		if(!fighter.king && !fighter.healthBar.activeSelf)
			currentTarget.GetComponent<Fighter> ().healthBar.SetActive (true);

		FighterStats target_stat = currentTarget.GetComponent<FighterStats> ();
		hp = target_stat.getHealth() - damage;
		target_stat.setHealth (hp);

		if (hitEffect != null) {
			GameObject hitE = (GameObject)Instantiate (hitEffect, currentTarget.transform.position, Quaternion.identity);
			Destroy (hitE, 1.5f);
		}
	}

	public bool hpCheck{get{ return hp <= 0; }}

	public void Dead(string tag){
		if (!currentTarget.CompareTag ("Tower") && !king) {
			resource.FighterReward (currentTarget);
			// Number effect
			resource.NumberEffect (currentTarget.transform, "+", currentTarget.GetComponent<FighterStats>().getGold(), Color.yellow);
		}
		if (king) {
			resource.KingReward (currentTarget);
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
