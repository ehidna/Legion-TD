using UnityEngine;

public class FighterStats : MonoBehaviour {

	[SerializeField]
	public float viewRadius;

	[SerializeField]
	private float range;
	public void setRange(float _range){
		range = _range;
	}
	public float getRange(){
		return range;
	}

	[SerializeField]
	private float health;
	public void setHealth(float _health){
		health = _health;
	}
	public float getHealth(){
		return health;
	}

	[SerializeField]
	private float damage;
	public void setDamage(float _damage){
		damage = _damage;
	}
	public float getDamage(){
		return damage;
	}


	[SerializeField]
	private float speed;
	public void setSpeed(float _speed){
		speed = _speed;
	}
	public float getSpeed(){
		return speed;
	}

	[SerializeField]
	private float projectileSpeed;
	public void setProjectileSpeed(float _ps){
		projectileSpeed = _ps;
	}
	public float getProjectileSpeed(){
		return projectileSpeed;
	}

	[SerializeField]
	private float turnSpeed;
	public void setTurnSpeed(float _turnSpeed){
		turnSpeed = _turnSpeed;
	}
	public float getTurnSpeed(){
		return turnSpeed;
	}

	[SerializeField]
	private float fireRate;
	public void setFireRate(float _fire){
		fireRate = _fire;
	}
	public float getFireRate(){
		return fireRate;
	}
}
