using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterController : MonoBehaviour {

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	[HideInInspector]
	public NavMeshAgent ag;

	public enum enemyStatus{Move, Fight, Idle};
	public enemyStatus currentStatus;

	[SerializeField]
	private string targetTag;

	Fighter _fighter;
	EnemySeeker seek;

	[Header("PATH")]
	[SerializeField]
	private Transform[] pathPoints;
	private int currentPathPoint;
	private Transform nextPathPoint;
	private float nextWaypointDistance = 1;

	void OnDisable(){
		currentStatus = enemyStatus.Idle;
		Reset (); //position, stat vb.
	}

	void Reset(){
		transform.position = GetComponent<FighterStats> ().getPosition ();
	}

	void Start () {
		ag = transform.GetComponent<NavMeshAgent> ();
		_fighter = transform.GetComponent<Fighter> ();
		seek = transform.GetComponent<EnemySeeker> ();
		currentPathPoint = 0;
		nextPathPoint = pathPoints [currentPathPoint];
	}

	void Update () {
		if (currentPathPoint < pathPoints.Length && currentStatus == enemyStatus.Move)
			Move ();
	}

	private void Move (){
		if (Vector3.Distance (transform.position, nextPathPoint.position) < nextWaypointDistance) {
			currentPathPoint++;
			if (currentPathPoint == pathPoints.Length) {
				return;
			}
		}
		//			FindNextPoint ();
		nextPathPoint = pathPoints [currentPathPoint];
		if (visibleTargets.Count == 0) {
			ag.SetDestination (nextPathPoint.position);
		} 
	}

	int FindNearestTarget(){
		int index = 0;
		float nearestTargetDst = Vector3.Distance (transform.position, visibleTargets [0].position);
		for (int i = 0; i < visibleTargets.Count; i++) {
			float dstToTarget = Vector3.Distance (transform.position, visibleTargets [i].position);
			if (nearestTargetDst > dstToTarget) {
				nearestTargetDst = dstToTarget;
				index = i;
			}
		}
		return index;
	}

	public void OnChildTriggerEnter(Collider other){
		if (other.CompareTag (targetTag)) {
			if (visibleTargets.Count == 0) {
				_fighter.currentTarget = other.gameObject;
				currentStatus = enemyStatus.Fight;
				ag.Stop ();
				seek.enabled = true;
			}
			visibleTargets.Add (other.gameObject.transform);
		}
	}

	public void OnChildTriggerExit(Collider other){
		if (other.CompareTag (targetTag)) {
			int i = visibleTargets.IndexOf (other.transform);
			visibleTargets.RemoveAt(i);

			if (visibleTargets.Count > 0) {// FindNearestTarget
				_fighter.currentTarget = visibleTargets [FindNearestTarget ()].gameObject;
				currentStatus = enemyStatus.Fight;
				ag.Stop ();
			}
			else {
				_fighter.currentTarget = null;
				seek.enabled = false;
				currentStatus = enemyStatus.Move;
				ag.Resume ();
			}
		}
	}
}
