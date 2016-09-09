using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour {

	public Text[] texts;

	public void WaveCleared(int gold, int level, float time, int income){
		texts[0].text = "Wave " + level.ToString() + " cleared!";
		texts[1].text = "You received " + gold.ToString() + " gold for completing the level.";
		texts[2].text = "You earned " + income.ToString() + " gold from your income.";
		texts[3].text = "Wave " + level.ToString() + " is beginning in "+ time.ToString()+ " seconds..";
		StartCoroutine ("ShowTime");
	}

	IEnumerator ShowTime(){
		
		yield return new WaitForSeconds (5);
		this.gameObject.SetActive (false);
	}
}
