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
	private int cost;
	public void setCost(int _cost){
		cost = _cost;
	}
	public int getCost(){
		return cost;
	}

	[SerializeField]
	private int income;
	public void setIncome(int _income){
		income = _income;
	}
	public int getIncome(){
		return income;
	}

	[SerializeField]
	private int gold;
	public void setGold(int _gold){
		gold = _gold;
	}
	public int getGold(){
		return gold;
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
	private float maxHealth;
	//	public void setMaxHealth(float _maxHealth){
	//		maxHealth = _maxHealth;
	//	}
	public float getMaxHealth(){
		return maxHealth;
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

	[SerializeField]
	private string damageType;
	public void setDamageType(string _dtype){
		damageType = _dtype;
	}
	public string getDamageType(){
		return damageType;
	}

	[SerializeField]
	private string armorType;
	public void setArmorType(string _atype){
		armorType = _atype;
	}
	public string getArmorType(){
		return armorType;
	}


}
