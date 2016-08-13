using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class WaveNumberText : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponent<Text>();
	}

	void Update () {
		text.text = WaveController.waveNumber.ToString();
	}
}