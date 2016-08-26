using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HUD : MonoBehaviour {

	public static HUD instance;

	[System.Serializable]
	public class Buildings{
		public GameObject build;
		public Button[] buttons;
	}

	public Buildings[] builds;
	void Awake (){
		if (instance != null){
			Debug.LogError("More than one HUD in scene.");
			return;
		}
		instance = this;
	}

	void Start () {
		foreach(Buildings item in builds)
			item.build.SetActive (false);
	}

	public void disableButtons(int index){
		for (int i = 0; i < builds.Length; i++) {
			if (i == index) 
				builds [i].build.SetActive (true);
			else 
				builds [i].build.SetActive (false);
		}

	}
}