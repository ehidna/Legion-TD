using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySeeker : MonoBehaviour {

	FighterStats stat;
	Fighter _fighter;
	FighterController fighter;
	FighterAnimator anim;
	NavMeshObstacle obs;

	void Start () {
		stat = GetComponent<FighterStats> (); 
		_fighter = GetComponent<Fighter> ();
		fighter = GetComponent<FighterController> ();
		anim = GetComponent<FighterAnimator> ();
		obs = GetComponent<NavMeshObstacle> ();
	}

	void Update () {	
		if (_fighter.currentTarget == null) {
			Seek ();
		}

		if (Vector3.Distance (transform.position, _fighter.currentTarget.transform.position) <=  Mathf.Sqrt(stat.getRange ()) ) {
			if(anim != null)
				anim.Idle ();
			_fighter.fight = true;
			fighter.ag.avoidancePriority = 30;
			fighter.ag.enabled = false;
			obs.enabled = true;
		} else {
			//			Vector3 dir =  _fighter.currentTarget.transform.position - transform.position;
			if(anim != null)
				anim.SetAnimBool ("walk", 1);
			//			transform.Translate(dir.normalized * fighter.ag.speed * Time.deltaTime, Space.World);
			_fighter.fight = false;
			obs.enabled = false;
			fighter.ag.enabled = true;
			fighter.SetDestination (_fighter.currentTarget.transform);

			fighter.ag.avoidancePriority = 50;
		}
	}
	void Seek(){
		if (fighter.visibleTargets.Count > 0) {
			_fighter.currentTarget = fighter.visibleTargets [FindNearestTarget ()].gameObject;
			fighter.currentStatus = FighterController.enemyStatus.Fight;
		} else {
			_fighter.currentTarget = null;
			fighter.currentStatus = FighterController.enemyStatus.Move;
		}
	}

	int FindNearestTarget(){
		fighter.visibleTargets.RemoveAll(target => target == null);
		int index = 0;
		float nearestTargetDst = Vector3.Distance (transform.position, fighter.visibleTargets [0].position);
		for (int i = 0; i < fighter.visibleTargets.Count; i++) {
			float dstToTarget = Vector3.Distance (transform.position, fighter.visibleTargets [i].position);
			if (nearestTargetDst > dstToTarget) {
				nearestTargetDst = dstToTarget;
				index = i;
			}
		}
		return index;
	}
}
