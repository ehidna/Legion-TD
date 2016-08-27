using UnityEngine;
using System.Collections;

public class ChooseFighter : MonoBehaviour {

	public void SetFighter(GameObject fighter){
		BuildManager.instance.SetFighter(fighter);
		BuildManager.instance.Node.GetComponent<Node> ().BuildFighter ();
	}
}
