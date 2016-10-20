using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	FighterStats stat;
	UIController ui;
	Town _town;

	public GameObject numberEffect;

	[SerializeField]
	private int money;
	public int Money{
		get{ return money;}
		set{ money = value;}
	}

	[SerializeField]
	private int lumber;
	public int Lumber{
		get{ return lumber;}
		set{ lumber = value;}
	}

	[SerializeField]
	private int income;
	public int Income{
		get{ return income;}
		set{ income = value;}
	}

	[SerializeField]
	private int kingKill;
	public int kingKills{
		get{ return kingKill;}
		set{ kingKill = value;}
	}

	void Start(){
		ui = GameObject.Find("UIManager").GetComponent<UIController>();
	}

	public void AddIncomeToMoney(){
		Money += Income;
		ui.RefreshMoney ();
	}

	public bool EnoughMoney(GameObject fighter){ 
		stat = fighter.GetComponent<FighterStats> ();
		return stat.getCost() <= Money; 
	}

	public void BuyTower(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Money -= stat.getCost ();
		ui.RefreshMoney ();
	}

	public void SellTower(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Money += stat.getCost () / 2;
		ui.RefreshMoney ();
	}

	public void NumberEffect(Transform _transform, string sign, int value, Color color){
		Vector3 pos = _transform.position;
		pos.y += 1; 
		GameObject effect = Instantiate (numberEffect, pos, numberEffect.transform.rotation) as GameObject;
		TextMesh text = effect.GetComponent<TextMesh> ();
		text.color = color;
		text.text  = sign + value;
		Destroy (effect, 1);
	}

	public void BuyMercenaries(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Lumber -= stat.getCost ();
		Income += stat.getIncome ();
		ui.RefreshLumber ();
		ui.RefreshIncome ();
	}

	public void BuyWorker(int value){
		Money -= value;
		ui.RefreshMoney ();
	}


	public void DecreaseCooldown(int value){
		Money -= value;
		Lumber -= value;
		ui.RefreshMoney ();
		ui.RefreshLumber ();
	}

	public void EmptyIncome(GameObject worker){
		_town = worker.GetComponent<Town> ();
		Lumber += _town.lumberIncome;
		ui.RefreshLumber ();
	}

	public void FighterReward(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Money += stat.getGold ();
		ui.RefreshMoney ();
	}

	public void KingReward(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		kingKills += stat.getGold ();
	}

	public void AddKingRewardToMoney (){
		Money += kingKills;
		ui.RefreshMoney ();
		kingKills = 0;
	}
}
