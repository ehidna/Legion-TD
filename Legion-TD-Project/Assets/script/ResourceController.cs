using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	FighterStats stat;
	UIController ui;

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

	void Start(){
		ui = GameObject.Find("UIManager").GetComponent<UIController>();
	}

	public void AddIncomeToMoney(){
		Money += Income;
		ui.RefreshMoney ();
	}

	public void BuyTower(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Money -= stat.getCost ();
		ui.RefreshMoney ();
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
		
	//	public void SellTower(GameObject fighter){ // tower satis
	//		stat = fighter.GetComponent<FighterStats> ();
	//		GameManager2.instance.money += stat.getCost () * 0.5f;
	//		ui.RefreshMoney ();
	//	}
	public void FighterReward(GameObject fighter){
		stat = fighter.GetComponent<FighterStats> ();
		Money += stat.getGold ();
		ui.RefreshMoney ();
	}

}
