using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private FighterStats parent;

	[SerializeField]
	private GameObject healthContainer;

	void Start () {
		parent = transform.GetComponentInParent<FighterStats> ();
	}

	void Update () {
		healthContainer.transform.localScale = new Vector3(Mathf.Clamp(parent.getHealth ()/parent.getMaxHealth(), .0f, 1.0f), 1, 1);
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}
}
