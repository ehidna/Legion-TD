using UnityEngine;
using System.Collections;

public class Radius : MonoBehaviour {

	private FighterController parent;
	void Start(){
		parent = transform.parent.GetComponent<FighterController>();
	}

	void OnTriggerEnter(Collider other){
		if(parent != null)
			parent.OnChildTriggerEnter(other);
	}
	void OnTriggerExit(Collider other){
		if(parent != null)
			parent.OnChildTriggerExit(other);
	}
}
