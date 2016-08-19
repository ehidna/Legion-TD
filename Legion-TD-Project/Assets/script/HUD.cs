using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HUD : MonoBehaviour {
	
	public static HUD instance;
	public GameObject image;
	private Button[] buttons;

	void Awake (){
		if (instance != null){
			Debug.LogError("More than one HUD in scene.");
			return;
		}
		instance = this;
	}

	void Start () {
		buttons = gameObject.GetComponent<Image>().GetComponentsInChildren<Button>();
		disableButtons ();
	}
	// Tum buttonlari aktif et
	public void activateButtons(){
		foreach(Button buton in buttons){
			buton.interactable = true;
		}
	}
	// Belirli buttonu aktif et
	public void activateButton(int item){
		for(int i=0; i<buttons.Length-4; i++){
			if(item == i)
				buttons[i].interactable = true;
		}
	}

	public void disableButtons(){
		foreach(Button buton in buttons){
			buton.image.color = Color.white;
			buton.interactable = false;
		}
	}

	public void setButtonImage(Color item, int position){
		// Button image or color add
		buttons [position].image.color = item;
		buttons [position].interactable = true;
	}
}