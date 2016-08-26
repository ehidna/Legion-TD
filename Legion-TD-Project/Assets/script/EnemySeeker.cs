using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySeeker : MonoBehaviour {

	FighterStats stat;
	Fighter _fighter;
	FighterController fighter;

	// Use this for initialization
	void Start () {
		stat = GetComponent<FighterStats> (); 
		_fighter = GetComponent<Fighter> ();
		fighter = GetComponent<FighterController> ();
	}

	// Update is called once per frame
	void Update () {	
		if (_fighter.currentTarget == null) {
			Seek ();
		}

		if (Vector3.Distance (transform.position, _fighter.currentTarget.transform.position) <=  Mathf.Sqrt(stat.getRange ()) ) {
			_fighter.fight = true;
		} else {
			Vector3 dir =  _fighter.currentTarget.transform.position - transform.position;
			transform.Translate(dir.normalized * fighter.ag.speed * Time.deltaTime, Space.World);
			_fighter.fight = false;
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
