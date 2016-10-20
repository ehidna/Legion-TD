using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class King : MonoBehaviour {

	private FighterStats stat;


	[SerializeField]
	private GameObject healthContainer;
	[SerializeField]
	private Text healthText;

	void Start () {
		stat = transform.GetComponent<FighterStats> ();
	}

	void Update () {
		healthContainer.transform.localScale = new Vector3(Mathf.Clamp(stat.getHealth ()/stat.getMaxHealth(), .0f, 1.0f), 1, 1);
		healthText.text = stat.getHealth() + "/" + stat.getMaxHealth(); 
	}
}
