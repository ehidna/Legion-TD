using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GameObject dragDropPlatform;

	public static GameManager instance;
	void Awake (){
		if (instance != null){
			Debug.Log("More than one GameManager in scene!");
			return;
		}
		instance = this;
	}

	void Start(){
		dragDropPlatform = GameObject.Find ("DragDropPlatform");	
	}

	[HideInInspector]
	public bool building;

	// Update is called once per frame
	void Update () {
		if (!building) {
			dragDropPlatform.SetActive (false);
			BuildManager.instance.enabled = false;
		} else {
			dragDropPlatform.SetActive (true);
			BuildManager.instance.enabled = true;
		}
	}
}
