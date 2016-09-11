using UnityEngine;
using System.Collections;

public class FighterAnimator : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void SetAnimBool(string state, float speed){
		anim.speed = speed;
		anim.Play (state);
		anim.SetBool("idle", false);
	}

	public void Idle(){
		anim.SetBool("idle", true);
	}

	public void StartPunch(){
		anim.SetBool ("IsPunching", true);
	}
	public void ExitPunch(){
		anim.SetBool ("IsPunching", false);
	}
}
