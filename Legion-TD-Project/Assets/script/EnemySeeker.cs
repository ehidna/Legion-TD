using UnityEngine;
using System.Collections;

public class EnemySeeker : MonoBehaviour {

	FighterStats stat;
	Fighter _fighter;

	// Use this for initialization
	void Start () {
		stat = GetComponent<FighterStats> (); 
		_fighter = GetComponent<Fighter> ();
	}

	// Update is called once per frame
	void Update () {	
		if (_fighter == null || _fighter.currentTarget == null) {
			return;
		}
		if (Vector3.Distance (transform.position, _fighter.currentTarget.transform.position) <= stat.getRange ()) {
			_fighter.fight = true;
		} else {
			Vector3 dir =  _fighter.currentTarget.transform.position - transform.position;
			transform.Translate(dir.normalized * stat.getSpeed() * Time.deltaTime, Space.World);
			_fighter.fight = false;
		}
	}
}
