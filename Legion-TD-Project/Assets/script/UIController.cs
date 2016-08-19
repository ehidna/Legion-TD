using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


	private Text moneyText;
	private Text lumberText;
	private Text incomeText;
	private Text AlertText;

	ResourceController rc;

	// Use this for initialization
	void Start () {
		moneyText = GameObject.Find ("Money").GetComponent<Text>();
		lumberText = GameObject.Find ("Lumber").GetComponent<Text>();
		incomeText = GameObject.Find ("Income").GetComponent<Text>();
		AlertText = GameObject.Find ("AlertText").GetComponent<Text>();
		rc = GameObject.Find ("ResourceManager").GetComponent<ResourceController> ();
	}

	public void RefreshMoney(){
		moneyText.text = "Money : " + rc.Money.ToString();
	}
	public void RefreshLumber(){
		lumberText.text = "Lumber : " + rc.Lumber.ToString();
	}
	public void RefreshIncome(){
		incomeText.text = "Income : " + rc.Income.ToString();
	}

	public void NoLumber(){
		AlertText.text = "Not enough Lumber!";
		StartCoroutine ("Delay");
	}

	public void NoMoney(){
		AlertText.text = "Not enough Money!";
		StartCoroutine ("Delay");
	}

	public void CantBuild(){
		AlertText.text = "Can't build there!";
		StartCoroutine ("Delay");
	}

	IEnumerator Delay(){
		AlertText.enabled = true;
		yield return new WaitForSeconds (1.5f);
		AlertText.enabled = false;
	}

}
