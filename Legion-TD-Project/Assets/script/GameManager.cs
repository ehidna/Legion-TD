using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	void Awake (){
		if (instance != null){
			Debug.Log("More than one GameManager in scene!");
			return;
		}
		instance = this;
	}

	[HideInInspector]
	public bool building;
	
	// Update is called once per frame
	void Update () {
//		if(BuildManager.instance == null)
//			return;
		if (!building)
			BuildManager.instance.enabled = false;
		else
			BuildManager.instance.enabled = true;
	}
}
