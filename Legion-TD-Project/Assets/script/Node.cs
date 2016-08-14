using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	private GameObject fighter;
	public Vector3 positionOffset;

	[SerializeField]
	private Text cantBuild;

	[HideInInspector]
	public Color color;

	void Start(){
		cantBuild = GameObject.FindGameObjectWithTag ("Build").GetComponent<Text>();
	}

	void Update(){
		if (fighter != null)
			color = Color.red;
		else
			color = Color.green;
	}

	IEnumerator MyCoroutine(){
		cantBuild.enabled = true;
		yield return new WaitForSeconds(1);
		cantBuild.enabled = false;
	}

	void OnMouseDown(){
		if (fighter != null){
			StartCoroutine(MyCoroutine());
			Debug.Log("Can't build there! - TODO: Display on screen.");
			return;
		}
		GameObject fighterToBuild = BuildManager.instance.GetFighterToBuild();
		fighter = (GameObject)Instantiate(fighterToBuild, transform.position + positionOffset, transform.rotation);
		fighter.GetComponent<FighterStats> ().setPosition (fighter.transform.position);
	}
}
