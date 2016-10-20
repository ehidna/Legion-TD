using UnityEngine;
using System.Collections;

public class EnemySeeker : MonoBehaviour {

	FighterStats stat;
	Fighter _fighter;
	FighterController fighterController;

	bool fighting;

	float counter = 2;
	float countdown;

	public bool king = false;

	void Start () {
		stat = GetComponent<FighterStats> (); 
		_fighter = GetComponent<Fighter> ();
		fighterController = GetComponent<FighterController> ();
	}

	void Update () {	
		if (_fighter.currentTarget == null) {
			fighting = false;  
			Seek ();
		} else {
			if (countdown == 2 && !fighting) {
				Seek ();
			}
			countdown -= Time.deltaTime;
			if (countdown < 0)
				countdown = counter;
		}

		if (Vector3.Distance (transform.position, _fighter.currentTarget.transform.position) <=  Mathf.Sqrt(stat.getRange ()) ) {
			fighting = true;
			fighterController.IdleAnim ();
			_fighter.fight = true;
			fighterController.FightAnimPrepare ();
		} else {
			_fighter.fight = false;
			if(!king)
				fighterController.WalkAnim (_fighter.currentTarget.transform);
		}
	}

	void Seek(){
		if (fighterController.visibleTargets.Count > 0) {
			_fighter.currentTarget = fighterController.visibleTargets [FindNearestTarget ()].gameObject;
			fighterController.currentStatus = FighterController.enemyStatus.Fight;
		} else {
			_fighter.currentTarget = null;
			if(!king)
				fighterController.currentStatus = FighterController.enemyStatus.Move;
			else 
				fighterController.currentStatus = FighterController.enemyStatus.Idle;
		}
	}

	int FindNearestTarget(){
		fighterController.visibleTargets.RemoveAll(target => target == null);
		int index = 0;
		float nearestTargetDst = Vector3.Distance (transform.position, fighterController.visibleTargets [0].position);
		for (int i = 0; i < fighterController.visibleTargets.Count; i++) {
			float dstToTarget = Vector3.Distance (transform.position, fighterController.visibleTargets [i].position);
			if (nearestTargetDst > dstToTarget) {
				nearestTargetDst = dstToTarget;
				index = i;
			}
		}
		return index;
	}
}
